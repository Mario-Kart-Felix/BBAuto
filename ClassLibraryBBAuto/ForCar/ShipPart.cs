using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ClassLibraryBBAuto
{
    public class ShipPart : MainDictionary
    {
        private int _idCar;
        private int _idDriver;
        private string _number;
        private DateTime _dateRequest;
        private DateTime _dateSent;
        private string _file;

        public string Number
        {
            get { return _number; }
            set { _number = value; }
        }

        public string File
        {
            get { return _file; }
            set { _file = value; }
        }

        public string IDCar
        {
            get { return _idCar.ToString(); }
            set { int.TryParse(value, out _idCar); }
        }

        public string IDDriver
        {
            get { return _idDriver.ToString(); }
            set { int.TryParse(value, out _idDriver); }
        }

        private Driver driver
        {
            get
            {
                DriverList driverList = DriverList.getInstance();
                return driverList.getItem(_idDriver);
            }
        }

        private Car car
        {
            get
            {
                CarList carList = CarList.getInstance();
                return carList.getItem(_idCar);
            }
        }

        public string DateRequest
        {
            get { return (_dateRequest == new DateTime(1, 1, 1)) ? string.Empty : _dateRequest.Date.ToShortDateString(); }
            set { DateTime.TryParse(value, out _dateRequest); }
        }

        private string DateRequestForSQL
        {
            get { return (_dateRequest == new DateTime(1, 1, 1)) ? string.Empty : _dateRequest.Year.ToString() + "-" + _dateRequest.Month.ToString() + "-" + _dateRequest.Day.ToString(); }
        }

        public string DateSent
        {
            get { return (_dateSent == new DateTime(1, 1, 1)) ? string.Empty : _dateSent.Date.ToShortDateString(); }
            set { DateTime.TryParse(value, out _dateSent); }
        }

        private string DateSentForSQL
        {
            get { return (_dateSent == new DateTime(1, 1, 1)) ? string.Empty : _dateSent.Year.ToString() + "-" + _dateSent.Month.ToString() + "-" + _dateSent.Day.ToString(); }
        }

        internal ShipPart(int idCar)
        {
            this._idCar = idCar;
        }

        internal ShipPart(DataRow row)
        {
            fillFields(row);
        }

        private void fillFields(DataRow row)
        {
            int.TryParse(row.ItemArray[0].ToString(), out _id);
            int.TryParse(row.ItemArray[1].ToString(), out _idCar);
            int.TryParse(row.ItemArray[2].ToString(), out _idDriver);
            _number = row.ItemArray[3].ToString();
            DateRequest = row.ItemArray[4].ToString();
            DateSent = row.ItemArray[5].ToString();
            _file = row.ItemArray[6].ToString();
            _fileBegin = _file;
        }

        internal override object[] getRow()
        {
            return new object[] { _id, _idCar, car.BBNumber, car.grz, driver.GetName(NameType.Full), _number, _dateRequest, _dateSent };
        }

        internal override void Delete()
        {
            _provider.Delete("ShipPart", _id);
        }

        internal bool isEqualCarID(Car car)
        {
            return car.IsEqualsID(_idCar);
        }

        public override void Save()
        {
            DeleteFile(_file);

            _file = WorkWithFiles.fileCopyByID(_file, "cars", _idCar, "ShipPart", _number);

            int.TryParse(_provider.Insert("ShipPart", _id, _idCar, _idDriver, _number, DateRequestForSQL, DateSentForSQL, _file).ToString(), out _id);
        }
    }
}
