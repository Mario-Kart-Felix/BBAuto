using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ClassLibraryBBAuto
{
    public class Invoice : MainDictionary
    {
        private const int DEFAULT_DRIVER_MEDIATOR = 2;

        private int _idCar;
        private int _idDriverFrom;
        private int _idDriverTo;
        private int _idRegionFrom;
        private int _idRegionTo;
        private DateTime _dateMove;
        private DateTime _date;

        public string file;
        
        public string DriverFromID
        {
            get { return _idDriverFrom.ToString(); }
            set { _idDriverFrom = Convert.ToInt32(value); }
        }

        public string DriverToID
        {
            get { return _idDriverTo.ToString(); }
            set { _idDriverTo = Convert.ToInt32(value); }
        }

        public string RegionFromID
        {
            get { return _idRegionFrom.ToString(); }
            set { _idRegionFrom = Convert.ToInt32(value); }
        }

        public string RegionToID
        {
            get { return _idRegionTo.ToString(); }
            set { _idRegionTo = Convert.ToInt32(value); }
        }

        public string DateMove
        {
            get { return (_dateMove.Year == 1) ? string.Empty : _dateMove.ToShortDateString(); }
            set { DateTime.TryParse(value, out _dateMove); }
        }

        public string DateMoveForSQL
        {
            get { return (_dateMove.Year == 1) ? string.Empty : 
                string.Concat(_dateMove.Year.ToString(), "-", _dateMove.Month.ToString(), "-", _dateMove.Day.ToString()); }
        }

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public Car car
        {
            get
            {
                CarList carList = CarList.getInstance();
                return carList.getItem(_idCar);
            }
        }

        internal Invoice(int idCar)
        {
            this._idCar = idCar;
            this._id = 0;
            name = getNextNumber();
            _date = DateTime.Today;

            fillNewInvoice();
        }

        public Invoice(DataRow row)
        {
            fillFields(row);
        }

        private void fillFields(DataRow row)
        {
            int.TryParse(row.ItemArray[0].ToString(), out _id);
            int.TryParse(row.ItemArray[1].ToString(), out _idCar);
            name = row.ItemArray[2].ToString();
            int.TryParse(row.ItemArray[3].ToString(), out _idDriverFrom);
            int.TryParse(row.ItemArray[4].ToString(), out _idDriverTo);
            DateTime.TryParse(row.ItemArray[5].ToString(), out _date);
            DateMove = row.ItemArray[6].ToString();
            int.TryParse(row.ItemArray[7].ToString(), out _idRegionFrom);
            int.TryParse(row.ItemArray[8].ToString(), out _idRegionTo);
            file = row.ItemArray[9].ToString();
            _fileBegin = file;
        }

        private void fillNewInvoice()
        {
            CarList carList = CarList.getInstance();
            Car car = carList.getItem(_idCar);

            InvoiceList invoiceList = InvoiceList.getInstance();
            Invoice invoice = invoiceList.getItem(car);

            if (invoice == null)
            {
                int.TryParse(car.regionUsingID.ToString(), out _idRegionFrom);
                _idDriverFrom = DEFAULT_DRIVER_MEDIATOR;
                int.TryParse(car.regionUsingID.ToString(), out _idRegionTo);
                int.TryParse(car.driverID.ToString(), out _idDriverTo);
            }
            else
            {
                _idRegionFrom = invoice._idRegionTo;
                _idDriverFrom = invoice._idDriverTo;
                _idRegionTo = 0;
                _idDriverTo = 0;
            }
        }

        private string getNextNumber()
        {
            InvoiceList invoiceList = InvoiceList.getInstance();
            int number = invoiceList.GetNextNumber();
            return number.ToString();
        }

        public override void Save()
        {
            DeleteFile(file);

            file = WorkWithFiles.fileCopyByID(file, "cars", _idCar, "Invoices", name);

            int.TryParse(_provider.Insert("Invoice", _id, _idCar, name, DriverFromID, DriverToID, _date, DateMoveForSQL, RegionFromID, RegionToID, file), out _id);
        }

        internal override object[] getRow()
        {
            Regions regions = Regions.getInstance();
            
            DriverList driverList = DriverList.getInstance();

            Driver driverFrom = driverList.getItem(_idDriverFrom);
            Driver driverTo = driverList.getItem(_idDriverTo);

            CarList carList = CarList.getInstance();
            Car car = carList.getItem(_idCar);

            return new object[11] { _id, _idCar, car.BBNumber, car.grz, name, regions.getItem(_idRegionFrom), driverFrom.GetName(NameType.Full),
                regions.getItem(_idRegionTo), driverTo.GetName(NameType.Full), _date, _dateMove };
        }

        internal override void Delete()
        {
            DeleteFile(file);

            _provider.Delete("Invoice", _id);
        }

        internal bool isEqualCarID(Car car)
        {
            return car.IsEqualsID(_idCar);
        }
    }
}
