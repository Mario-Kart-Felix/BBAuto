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
        public DateTime date;
        public string file;
        private int _notifacationSent;

        public bool IsNotificationSent
        {
            get { return Convert.ToBoolean(_notifacationSent); }
            private set { _notifacationSent = Convert.ToInt32(value); }
        }

        internal DiagCard(int idCar)
        {
            _id = 0;
            _idCar = idCar;
            name = string.Empty;
            date = DateTime.Today;
            file = string.Empty;
        }

        public DiagCard(DataRow row)
        {
            fillFields(row);
        }

        private void fillFields(DataRow row)
        {
            int.TryParse(row.ItemArray[0].ToString(), out _id);
            int.TryParse(row.ItemArray[1].ToString(), out _idCar);
            name = row.ItemArray[2].ToString();
            DateTime.TryParse(row.ItemArray[3].ToString(), out date);
            file = row.ItemArray[4].ToString();
            _fileBegin = file;
            int.TryParse(row.ItemArray[5].ToString(), out _notifacationSent);
        }

        public override void Save()
        {
            DeleteFile(file);

            file = WorkWithFiles.fileCopyByID(file, "cars", _idCar, "DiagCard", name);

            ExecSave();
        }

        private void ExecSave()
        {
            int.TryParse(_provider.Insert("DiagCard", _id, _idCar, name, date, file, _notifacationSent), out _id);
        }

        internal override void Delete()
        {
            DeleteFile(file);

            _provider.Delete("DiagCard", _id);
        }

        internal override object[] getRow()
        {
            return new object[] { _id, _idCar, GetCar().BBNumber, GetCar().grz, name, date };
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
            sb.Append(name);
            sb.Append(" ");
            sb.Append(date.ToShortDateString());
            return sb.ToString();
        }

        public Car GetCar()
        {
            CarList carList = CarList.getInstance();
            return carList.getItem(_idCar);
        }
    }
}
