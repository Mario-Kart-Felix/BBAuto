using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ClassLibraryBBAuto
{
    public class DriverLicense : MainDictionary, INotification, IActual
    {
        private int _idDriver;
        private DateTime _dateBegin;
        private DateTime _dateEnd;
        private int _notificationSent;

        private string _file;

        public DateTime DateBegin
        {
            get { return _dateBegin; }
            set { _dateBegin = value; }
        }

        public DateTime DateEnd
        {
            get { return _dateEnd; }
            set { _dateEnd = value; }
        }

        public bool IsNotificationSent
        {
            get { return Convert.ToBoolean(_notificationSent); }
            private set { _notificationSent = Convert.ToInt32(value); }
        }

        public int DriverID { get { return _idDriver; } }

        public string File
        {
            get { return _file; }
            set { _file = value; }
        }

        public DriverLicense(int idDriver)
        {
            _id = 0;
            _idDriver = idDriver;

            _dateBegin = DateTime.Today;
            _dateEnd = DateTime.Today;

            _file = "";
        }

        public DriverLicense(DataRow row)
        {
            fillFields(row);
        }

        private void fillFields(DataRow row)
        {
            int.TryParse(row.ItemArray[0].ToString(), out _id);
            int.TryParse(row.ItemArray[1].ToString(), out _idDriver);
            name = row.ItemArray[2].ToString();
            DateTime.TryParse(row.ItemArray[3].ToString(), out _dateBegin);
            DateTime.TryParse(row.ItemArray[4].ToString(), out _dateEnd);
            _file = row.ItemArray[5].ToString();
            _fileBegin = _file;
            int.TryParse(row.ItemArray[6].ToString(), out _notificationSent);
        }

        public override void Save()
        {
            DeleteFile(_file);

            _file = WorkWithFiles.fileCopyByID(_file, "drivers", _idDriver, "DriverLicense", name);

            ExecSave();

            LicenseList licenseList = LicenseList.getInstance();
            licenseList.Add(this);
        }

        private void ExecSave()
        {
            int.TryParse(_provider.Insert("DriverLicense", _id, _idDriver, name, _dateBegin, _dateEnd, _file, _notificationSent), out _id);
        }

        internal override object[] getRow()
        {
            return new object[3] { _id, name, _dateEnd.ToShortDateString() };
        }

        internal override void Delete()
        {
            WorkWithFiles.Delete(_file);

            _provider.Delete("DriverLicense", _id);
        }

        public bool idEqualDriverID(Driver driver)
        {
            return driver.IsEqualsID(_idDriver);
        }

        public override string ToString()
        {
            return _idDriver == 0 ? "нет данных" : string.Concat("№", name, " до ", _dateEnd.ToShortDateString());
        }

        public void SendNotification()
        {
            DriverList driverList = DriverList.getInstance();
            Driver driver = driverList.getItem(_idDriver);
            
            string message = CreateMessageNotification();

            eMail email = new eMail();
            email.SendNotification(driver, message);

            IsNotificationSent = true;
            if (_id != 0)
                ExecSave();
        }

        private string CreateMessageNotification()
        {
            DriverList driverList = DriverList.getInstance();
            Driver driver = driverList.getItem(_idDriver);

            if (!IsHaveFile())
            {
                return "Добрый день, " + driver.GetName(NameType.Full) + "!\r\n\r\nПросьба предоставить скан копию Вашего водительского удостоверения в транспортный отдел.\r\n\r\nС уважением,\r\nТранспортный отдел.";
            }

            MailTextList mailTextList = MailTextList.getInstance();
            MailText mailText = mailTextList.getItemByType(MailTextType.License);

            return mailText == null ? "Шаблон текста письма не найден" : mailText.Text.Replace("UserName", driver.GetName(NameType.Full)).Replace("DateEnd", DateEnd.ToShortDateString());
        }

        public bool IsActual()
        {
            return IsHaveFile() && IsDateActual();
        }

        public bool IsDateActual()
        {
            return _dateEnd > DateTime.Today;
        }
        
        public bool IsHaveFile()
        {
            return File != string.Empty;
        }
    }
}
