using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ClassLibraryBBAuto
{
    public class DTP : MainDictionary
    {
        private int idCar;        
                
        private int _idStatusAfterDTP;
        private int _idRegion;
        private int _idCulprit;
        private double _sum;
        private int _number;
        private DateTime _dateCallInsure;
        private DateTime _date;
        private int _idCurrentStatusAfterDTP;

        public string facts;
        public string damage;
        public string comm;        
        public string numberLoss;        

        public string IDStatusAfterDTP
        {
            get { return _idStatusAfterDTP.ToString(); }
            set { int.TryParse(value, out _idStatusAfterDTP); }
        }

        public object IDcurrentStatusAfterDTP
        {
            get { return _idCurrentStatusAfterDTP.ToString(); }
            set
            {
                if (value != null)
                    int.TryParse(value.ToString(), out _idCurrentStatusAfterDTP);
            }
        }

        public string IDRegion
        {
            get { return _idRegion.ToString(); }
            set { int.TryParse(value, out _idRegion); }
        }

        public string IDCulprit
        {
            get { return _idCulprit.ToString(); }
            set { int.TryParse(value, out _idCulprit); }
        }

        public int Number { get { return _number; } }

        public string Sum
        {
            get { return _sum.ToString(); }
            set { double.TryParse(value.Replace(" ", "").Replace(".", ","), out _sum); }
        }
        
        public string DateCallInsure
        {
            get { return (_dateCallInsure.Year == 1) ? string.Empty : _dateCallInsure.ToShortDateString(); }
            set { DateTime.TryParse(value, out _dateCallInsure); }
        }

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }
        
        public DTP(int idCar)
        {            
            _id = 0;
            this.idCar = idCar;
            _idStatusAfterDTP = 0;
            _idRegion = 0;
            _date = DateTime.Now;
            _dateCallInsure = DateTime.Now;
        }

        public DTP(DataRow row)
        {
            int.TryParse(row.ItemArray[0].ToString(), out _id);
            int.TryParse(row.ItemArray[1].ToString(), out idCar);
            int.TryParse(row.ItemArray[2].ToString(), out _number);
            DateTime.TryParse(row.ItemArray[3].ToString(), out _date);
            int.TryParse(row.ItemArray[4].ToString(), out _idRegion);
            DateTime.TryParse(row.ItemArray[5].ToString(), out _dateCallInsure);
            int.TryParse(row.ItemArray[6].ToString(), out _idCulprit);
            IDStatusAfterDTP = row.ItemArray[7].ToString();
            numberLoss = row.ItemArray[8].ToString();
            double.TryParse(row.ItemArray[9].ToString(), out _sum);
            damage = row.ItemArray[10].ToString();
            facts = row.ItemArray[11].ToString();
            comm = row.ItemArray[12].ToString();
            IDcurrentStatusAfterDTP = row.ItemArray[13].ToString();
        }
        
        public override void Save()
        {
            int.TryParse(_provider.Insert("DTP", _id, idCar, _date, _idRegion, _dateCallInsure, IDCulprit, IDStatusAfterDTP, numberLoss, _sum, damage, facts, comm, IDcurrentStatusAfterDTP), out _id);
            
            DTPList dtpList = DTPList.getInstance();
            dtpList.Add(this);

            if (_number == 0)
                _number = dtpList.GetMaxNumber() + 1;
        }
        
        private DataTable getCulpritDataTable()
        {
            return _provider.DoOther("exec Culprit_SelectWithUser @p1, @p2", idCar, _date);
        }

        internal override void Delete()
        {
            _provider.Delete("DTP", _id);
        }
        
        internal override object[] getRow()
        {
            Regions regions = Regions.getInstance();
            
            Car car = getCar();

            Culprits culpritList = Culprits.getInstance();
            StatusAfterDTPs statusAfterDTP = StatusAfterDTPs.getInstance();
            
            Driver driver = GetDriver();

            return new object[] {_id, idCar, car.BBNumber, car.grz, _number, _date, regions.getItem(_idRegion), driver.GetName(NameType.Full),
                _dateCallInsure, GetCurrentStatusAfterDTP(), culpritList.getItem(_idCulprit), _sum, comm, facts, damage, 
                statusAfterDTP.getItem(_idStatusAfterDTP), numberLoss };
        }

        public Car getCar()
        {
            CarList carList = CarList.getInstance();
            return carList.getItem(idCar);
        }

        internal bool isEqualCarID(Car car)
        {
            return car.IsEqualsID(idCar);
        }

        internal bool isEqualDriverID(Driver driver)
        {
            Driver driver2 = GetDriver();

            return driver.Equals(driver2);
        }

        public override string ToString()
        {
            return (idCar == 0) ? "нет данных" : string.Concat("№", name, " дата ", _date.ToShortDateString());
        }

        public DTPFile createFile()
        {
            return new DTPFile(_id);
        }

        internal object[] getCulpit()
        {
            DriverCarList driverCarList = DriverCarList.getInstance();
            CarList carList = CarList.getInstance();
            Driver driver = driverCarList.GetDriver(carList.getItem(idCar), _date);

            return new object[] { 4, driver.GetName(NameType.Full) };
        }

        public Driver GetDriver()
        {
            Car car = getCar();
            DriverCarList driverCarList = DriverCarList.getInstance();
            return driverCarList.GetDriver(car, _date);
        }

        public string GetCurrentStatusAfterDTP()
        {
            CurrentStatusAfterDTPs currentStatusAfterDTPs = CurrentStatusAfterDTPs.getInstance();
            return currentStatusAfterDTPs.getItem(_idCurrentStatusAfterDTP);
        }
    }
}
