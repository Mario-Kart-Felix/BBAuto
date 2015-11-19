using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ClassLibraryBBAuto
{
    public class DiagCard : MainDictionary
    {
        private int _idCar;
        private string _number;
        private DateTime _date;
        private string _file;
        private int _notifacationSent;

        public string Number
        {
            get { return _number; }
            set { _number = value; }
        }

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public bool IsNotificationSent
        {
            get { return Convert.ToBoolean(_notifacationSent); }
            private set { _notifacationSent = Convert.ToInt32(value); }
        }

        public string File
        {
            get { return _file; }
            set { _file = value; }
        }

        internal DiagCard(int idCar)
        {
            _id = 0;
            _idCar = idCar;
            _number = string.Empty;
            _date = DateTime.Today;
            _file = string.Empty;
        }

        public DiagCard(DataRow row)
        {
            fillFields(row);
        }

        private void fillFields(DataRow row)
        {
            int.TryParse(row.ItemArray[0].ToString(), out _id);
            int.TryParse(row.ItemArray[1].ToString(), out _idCar);
            _number = row.ItemArray[2].ToString();
            DateTime.TryParse(row.ItemArray[3].ToString(), out _date);
            _file = row.ItemArray[4].ToString();
            _fileBegin = _file;
            int.TryParse(row.ItemArray[5].ToString(), out _notifacationSent);
        }

        public override void Save()
        {
            DeleteFile(_file);

            _file = WorkWithFiles.fileCopyByID(_file, "cars", _idCar, "DiagCard", _number);

            ExecSave();
        }

        private void ExecSave()
        {
            int.TryParse(_provider.Insert("DiagCard", _id, _idCar, _number, _date, _file, _notifacationSent), out _id);
        }

        internal override void Delete()
        {
            DeleteFile(_file);

            _provider.Delete("DiagCard", _id);
        }

        internal override object[] getRow()
        {
            return new object[] { _id, _idCar, GetCar().BBNumber, GetCar().grz, _number, _date };
        }

        internal bool isEqualsCarID(Car car)
        {
            return car.IsEqualsID(_idCar);
        }

        internal string ToMail()
        {
            IsNotificationSent = true;
            ExecSave();

            CarList carList = CarList.getInstance();
            Car car = carList.getItem(_idCar);

            StringBuilder sb = new StringBuilder();
            sb.Append(car.grz);
            sb.Append(" ");
            sb.Append(_number);
            sb.Append(" ");
            sb.Append(_date.ToShortDateString());
            return sb.ToString();
        }

        public Car GetCar()
        {
            CarList carList = CarList.getInstance();
            return carList.getItem(_idCar);
        }
    }
}
