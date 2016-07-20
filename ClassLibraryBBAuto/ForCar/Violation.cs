using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BBAuto.Domain
{
    public class Violation : MainDictionary
    {
        private readonly DateTime default_date = new DateTime(1, 1, 1);

        private int idCar;
        private int _sum;
        private int _idViolationType;
        private int _sent;
        private string _fileBeginPay;
        private DateTime? _datePay;
        private int _noDeduction;

        public string Number { get; set; }
        public DateTime Date { get; set; }
        public string FilePay { get; set; }
        public string File { get; set; }
        public bool Agreed { get; private set; }
        public DateTime DateCreate { get; private set; }
        
        public DateTime? DatePay
        {
            get { return _datePay; }
            set
            {
                _datePay = value;

                if (_datePay != null)
                {
                    Agreed = true;
                }
            }
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

        public bool NoDeduction
        {
            get { return Convert.ToBoolean(_noDeduction); }
            set { _noDeduction = Convert.ToInt32(value); }
        }

        public Violation() { }

        public Violation(int idCar)
        {
            this.idCar = idCar;
            Date = DateTime.Today;
            _datePay = DateTime.Today;
            File = string.Empty;
            FilePay = string.Empty;
        }

        public Violation(object[] row)
        {
            FillFields(row);
        }

        private void FillFields(object[] row)
        {
            int.TryParse(row[0].ToString(), out _id);
            int.TryParse(row[1].ToString(), out idCar);

            DateTime date;
            DateTime.TryParse(row[2].ToString(), out date);
            Date = date;

            Number = row[3].ToString();
            File = row[4].ToString();
            _fileBegin = File;

            DateTime datePay;
            DateTime.TryParse(row[5].ToString(), out datePay);
            if (datePay != default_date)
                DatePay = datePay;

            FilePay = row[6].ToString();
            _fileBeginPay = FilePay;

            int.TryParse(row[7].ToString(), out _idViolationType);
            int.TryParse(row[8].ToString(), out _sum);
            int.TryParse(row[9].ToString(), out _sent);
            int.TryParse(row[10].ToString(), out _noDeduction);

            bool agreed;
            bool.TryParse(row[11].ToString(), out agreed);
            Agreed = agreed;

            DateTime dateCreate;
            DateTime.TryParse(row[12].ToString(), out dateCreate);
            DateCreate = new DateTime(dateCreate.Year, dateCreate.Month, dateCreate.Day);
        }

        public override void Save()
        {
            DeleteFile(File);
            deleteFilePay();

            File = WorkWithFiles.fileCopyByID(File, "cars", idCar, "Violation", Number);
            FilePay = WorkWithFiles.fileCopyByID(FilePay, "cars", idCar, "ViolationPay", Number);

            int.TryParse(_provider.Insert("Violation", _id, idCar, Date, Number, File, (DatePay == null) ? string.Empty : DatePay.Value.ToShortDateString(),
                FilePay, _idViolationType, _sum, _sent, _noDeduction, Agreed.ToString()), out _id);
        }
        
        internal override void Delete()
        {
            DeleteFile(File);
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

            return new object[] { _id, idCar, car.BBNumber, car.Grz, regionName, Date, driver.GetName(NameType.Full), Number, DatePay, 
                violationType.getItem(_idViolationType), _sum };
        }

        internal object[] GetRowAccount()
        {
            string btnName = (Agreed) ? string.Empty : "Согласовать";
            string btnFile = (string.IsNullOrEmpty(File)) ? string.Empty : "Просмотр";
            
            return new object[]
            {
                _id,
                idCar,
                Number,
                Date,
                getDriver().GetName(NameType.Full),
                ViolationTypes.getInstance().getItem(_idViolationType),
                _sum,          
                btnName,
                btnFile
            };
        }
        
        

        internal bool isEqualDriverID(Driver driver)
        {
            CarList carList = CarList.getInstance();
            Car car = carList.getItem(idCar);
            DriverList driverList = DriverList.getInstance();

            DriverCarList driverCarList = DriverCarList.getInstance();
            Driver driverDTP = driverCarList.GetDriver(car, Date);

            return driver.Equals(driverDTP);
        }

        public override string ToString()
        {
            return (idCar == 0) ? "нет данных" : string.Concat("№", Number, " от ", Date.ToShortDateString());
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
            Driver driver = driverCarList.GetDriver(car, Date);

            return driver ?? new Driver();
        }

        protected void deleteFilePay()
        {
            if ((_fileBeginPay != string.Empty) && (_fileBeginPay != FilePay))
                WorkWithFiles.Delete(_fileBeginPay);
        }

        public void Agree()
        {
            eMail email = new eMail();
            email.SendMailAccountViolation(this);
            
            Agreed = true;

            Save();
        }
    }
}
