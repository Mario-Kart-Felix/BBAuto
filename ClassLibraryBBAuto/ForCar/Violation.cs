using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ClassLibraryBBAuto
{
    public class Violation : MainDictionary
    {
        private int idCar;
        private string _number;
        private int _isPaid;
        private int _sum;
        private int _idViolationType;
        private int _sent;
        private string _fileBeginPay;

        private DateTime _date;
        private DateTime _datePay;
        private string _file;
        private string _filePay;
        private int _noDeduction;

        public string Number
        {
            get { return _number; }
            set { _number = value; }
        }

        public bool IsPaid
        {
            get { return Convert.ToBoolean(_isPaid); }
            set { _isPaid = Convert.ToInt32(value); }
        }

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public DateTime DatePay
        {
            get { return IsPaid ? _datePay : new DateTime(1, 1, 1); }
            set { _datePay = value; }
        }

        public string Sum
        {
            get { return _sum == 0 ? string.Empty : _sum.ToString(); }
            set { int.TryParse(value, out _sum); }
        }

        public string IDViolationType
        {
            get { return _idViolationType.ToString(); }
            set { int.TryParse(value, out _idViolationType); }
        }

        public bool Sent
        {
            get { return Convert.ToBoolean(_sent); }
            set { _sent = Convert.ToInt32(value); }
        }

        public string FilePay
        {
            get { return _filePay; }
            set { _filePay = value; }
        }

        public string File
        {
            get { return _file; }
            set { _file = value; }
        }

        public bool NoDeduction
        {
            get { return Convert.ToBoolean(_noDeduction); }
            set { _noDeduction = Convert.ToInt32(value); }
        }

        public Violation(int idCar)
        {
            this.idCar = idCar;
            _date = DateTime.Today;
            _datePay = DateTime.Today;
            _file = string.Empty;
            _filePay = string.Empty;
        }

        public Violation(DataRow row)
        {
            fillFields(row);
        }

        private void fillFields(DataRow row)
        {
            int.TryParse(row.ItemArray[0].ToString(), out _id);
            int.TryParse(row.ItemArray[1].ToString(), out idCar);
            DateTime.TryParse(row.ItemArray[2].ToString(), out _date);
            _number = row.ItemArray[3].ToString();
            _file = row.ItemArray[4].ToString();
            _fileBegin = _file;

            if (row.ItemArray[5].ToString() == string.Empty)
            {
                IsPaid = false;
                _datePay = DateTime.Today;
                _filePay = string.Empty;
            }
            else
            {
                IsPaid = true;
                DateTime.TryParse(row.ItemArray[5].ToString(), out _datePay);
                _filePay = row.ItemArray[6].ToString();
            }

            _fileBeginPay = _filePay;

            int.TryParse(row.ItemArray[7].ToString(), out _idViolationType);
            int.TryParse(row.ItemArray[8].ToString(), out _sum);
            int.TryParse(row.ItemArray[9].ToString(), out _sent);
            int.TryParse(row.ItemArray[10].ToString(), out _noDeduction);
        }

        public override void Save()
        {
            DeleteFile(_file);
            deleteFilePay();

            _file = WorkWithFiles.fileCopyByID(_file, "cars", idCar, "Violation", _number);
            _filePay = WorkWithFiles.fileCopyByID(_filePay, "cars", idCar, "ViolationPay", _number);

            int.TryParse(_provider.Insert("Violation", _id, idCar, _date, _number, _isPaid, _file, _datePay, _filePay, _idViolationType, _sum, _sent, _noDeduction), out _id);
        }
        
        internal override void Delete()
        {
            DeleteFile(_file);
            deleteFilePay();

            _provider.Delete("Violation", _id);
        }

        internal override object[] getRow()
        {
            Car car = getCar();
            Driver driver = getDriver();

            ViolationTypes violationType = ViolationTypes.getInstance();

            InvoiceList invoiceList = InvoiceList.getInstance();
            Invoice invoice = invoiceList.getItem(car);
            Regions regions = Regions.getInstance();
            string regionName = (invoice == null) ? regions.getItem(Convert.ToInt32(car.regionUsingID)) : regions.getItem(Convert.ToInt32(invoice.RegionToID));

            return new object[] { _id, idCar, car.BBNumber, car.grz, regionName, _date, driver.GetName(NameType.Full), _number, DatePay, 
                violationType.getItem(_idViolationType), _sum };
        }

        internal bool isEqualCarID(Car car)
        {
            return car.IsEqualsID(idCar);
        }

        internal bool isEqualDriverID(Driver driver)
        {
            CarList carList = CarList.getInstance();
            Car car = carList.getItem(idCar);
            DriverList driverList = DriverList.getInstance();

            DriverCarList driverCarList = DriverCarList.getInstance();
            Driver driverDTP = driverCarList.GetDriver(car, _date);

            return driver.Equals(driverDTP);
        }

        public override string ToString()
        {
            return (idCar == 0) ? "нет данных" : string.Concat("№", _number, " от ", _date.ToShortDateString());
        }

        public Car getCar()
        {
            CarList carList = CarList.getInstance();
            Car car = carList.getItem(idCar);
            
            return car;
        }

        public Driver getDriver()
        {
            Car car = getCar();
            
            DriverCarList driverCarList = DriverCarList.getInstance();
            Driver driver = driverCarList.GetDriver(car, _date);

            return driver ?? new Driver();
        }

        protected void deleteFilePay()
        {
            if ((_fileBeginPay != string.Empty) && (_fileBeginPay != _filePay))
                WorkWithFiles.Delete(_fileBeginPay);
        }
    }
}
