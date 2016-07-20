using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BBAuto.Domain
{
    public class Repair : MainDictionary
    {
        private int _idRepairType;
        private int _idServiceStantion;
        private double _cost;
        private int _idCar;
        private DateTime _date;
        private string _file;

        public string CarID { get { return _idCar.ToString(); } }

        public string RepairTypeID
        {
            get { return _idRepairType.ToString(); }
            set { int.TryParse(value, out _idRepairType); }
        }

        public string ServiceStantionID
        {
            get { return _idServiceStantion.ToString(); }
            set { int.TryParse(value, out _idServiceStantion); }
        }

        public string Cost
        {
            get { return _cost.ToString(); }
            set { double.TryParse(value, out _cost); }
        }

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public string File
        {
            get { return _file; }
            set { _file = value; }
        }

        public Repair(int idCar)
        {
            _id = 0;
            this._idCar = idCar;
            _date = DateTime.Today;
        }

        public Repair(int idCar, DataRow row)
        {
            this._idCar = idCar;

            fillFields(row);
        }

        public Repair(DataRow row)
        {
            fillFields(row);
        }

        private void fillFields(DataRow row)
        {
            int.TryParse(row.ItemArray[0].ToString(), out _id);
            int.TryParse(row.ItemArray[1].ToString(), out _idCar);
            int.TryParse(row.ItemArray[2].ToString(), out _idRepairType);
            int.TryParse(row.ItemArray[3].ToString(), out _idServiceStantion);
            DateTime.TryParse(row.ItemArray[4].ToString(), out _date);
            Cost = row.ItemArray[5].ToString();
            _file = row.ItemArray[6].ToString();
            _fileBegin = _file;
        }

        internal override object[] getRow()
        {
            string show = "";
            if (_file != string.Empty)
                show = "Показать";

            CarList carList = CarList.getInstance();
            Car car = carList.getItem(_idCar);

            RepairTypes repairTypes = RepairTypes.getInstance();
            ServiceStantions serviceStantions = ServiceStantions.getInstance();

            return new object[] { _id, _idCar, car.BBNumber, car.Grz, repairTypes.getItem(_idRepairType), serviceStantions.getItem(_idServiceStantion),
                _date, _cost, show };
        }

        public override void Save()
        {
            if (_id == 0)
                int.TryParse(_provider.Insert("Repair", _id, _idCar, _idRepairType, _idServiceStantion, _date, _cost, _file), out _id);

            DeleteFile(_file);

            _file = WorkWithFiles.fileCopyByID(_file, "cars", _idCar, "Repair", _id.ToString());
            int.TryParse(_provider.Insert("Repair", _id, _idCar, _idRepairType, _idServiceStantion, _date, _cost, _file), out _id);
        }

        internal override void Delete()
        {
            DeleteFile(_file);

            _provider.Delete("Repair", _id);
        }

        internal bool isEqualCarID(Car car)
        {
            return car.IsEqualsID(_idCar);
        }
    }
}
