using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BBAuto.Domain
{
    public class MedicalCert : MainDictionary, INotification, IActual
    {
        private int _idDriver;
        private string _number;
        private DateTime _dateBegin;
        private DateTime _dateEnd;
        private int _notifacationSent;
        private string _file;

        public string Number
        {
            get { return _number; }
            set { _number = value; }
        }

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
            get { return Convert.ToBoolean(_notifacationSent); }
            private set { _notifacationSent = Convert.ToInt32(value); }
        }

        public int DriverID { get { return _idDriver; } }

        public string File
        {
            get { return _file; }
            set { _file = value; }
        }

        public MedicalCert(int idDriver)
        {
            _idDriver = idDriver;
            _id = 0;
            _dateBegin = DateTime.Today;
            _dateEnd = DateTime.Today;

            File = string.Empty;
        }

        public MedicalCert(DataRow row)
        {
            fillFields(row);
        }

        private void fillFields(DataRow row)
        {
            int.TryParse(row.ItemArray[0].ToString(), out _id);
            _number = row.ItemArray[1].ToString();
            DateTime.TryParse(row.ItemArray[2].ToString(), out _dateBegin);
            DateTime.TryParse(row.ItemArray[3].ToString(), out _dateEnd);
            int.TryParse(row.ItemArray[4].ToString(), out _idDriver);
            _file = row.ItemArray[5].ToString();
            _fileBegin = _file;
            int.TryParse(row.ItemArray[6].ToString(), out _notifacationSent);
        }

        public override void Save()
        {
            DeleteFile(File);

            File = WorkWithFiles.fileCopyByID(File, "drivers", _idDriver, "MedicalCert", _number);
            
            ExecSave();

            MedicalCertList medicalCertList = MedicalCertList.getInstance();
            medicalCertList.Add(this);
        }

        private void ExecSave()
        {
            int.TryParse(_provider.Insert("MedicalCert", _id, _idDriver, _number, _dateBegin, _dateEnd, File, _notifacationSent), out _id);
        }
        
        internal override void Delete()
        {
            DeleteFile(File);

            _provider.Delete("MedicalCert", _id);
        }

        internal override object[] getRow()
        {
            return new object[3] { _id, _number, _dateEnd.ToShortDateString() };
        }

        internal bool idEqualDriverID(Driver driver)
        {
            return driver.IsEqualsID(_idDriver);
        }

        public override string ToString()
        {
            return _idDriver == 0 ? "нет данных" : string.Concat(_number, " до ", _dateEnd.ToShortDateString());
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

            if (_id == 0)
            {
                return "Добрый день, " + driver.GetName(NameType.Full) + "!\r\n\r\nНапоминаем, что Вы своевременно не оформили водительскую медицинскую справку.\r\nПросим оформить данную справку.\r\nОригинал необходимо прислать в отдел кадров, а скан копию в транспортный отдел.\r\n\r\nС уважением,\r\nТранспортный отдел.";
            }
            
            MailTextList mailTextList = MailTextList.getInstance();
            MailText mailText = mailTextList.getItemByType(MailTextType.MedicalCert);

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
