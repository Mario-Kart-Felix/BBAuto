using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ClassLibraryBBAuto
{
    public class Passport : MainDictionary
    {
        private int _idDriver;
        private DateTime _giveDate;

        private string _lastName;
        private string _firstName;
        private string _secondName;
        private string _number;
        private string _giveOrg;
        private string _address;
        private string _file;

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string SecondName
        {
            get { return _secondName; }
            set { _secondName = value; }
        }

        public string Number
        {
            get { return _number; }
            set { _number = value; }
        }

        public string GiveOrg
        {
            get { return _giveOrg; }
            set { _giveOrg = value; }
        }

        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        public string File
        {
            get { return _file; }
            set { _file = value; }
        }

        public string GiveDate
        {
            get
            {
                return _giveDate.ToShortDateString();
            }
            set
            {
                DateTime date;                
                DateTime.TryParse(value, out date);
                _giveDate = date;

                if (date.Year == 1)
                    _giveDate = DateTime.Today;
            }
        }

        public Passport(int idDriver)
        {
            _id = 0;
            _idDriver = idDriver;
            _giveDate = DateTime.Today;
            _number = string.Empty;
        }

        public Passport(DataRow row)
        {
            fillFields(row);
        }

        private void fillFields(DataRow row)
        {
            int.TryParse(row.ItemArray[0].ToString(), out _id);
            int.TryParse(row.ItemArray[1].ToString(), out _idDriver);
            _lastName = row.ItemArray[2].ToString();
            _firstName = row.ItemArray[3].ToString();
            _secondName = row.ItemArray[4].ToString();
            _number = row.ItemArray[5].ToString();
            _giveOrg = row.ItemArray[6].ToString();
            GiveDate = row.ItemArray[7].ToString();
            _address = row.ItemArray[8].ToString();
            _file = row.ItemArray[9].ToString();
            _fileBegin = _file;
        }

        public override void Save()
        {
            DeleteFile(_file);

            _file = WorkWithFiles.fileCopyByID(_file, "drivers", _idDriver, "Passports", _number);

            int.TryParse(_provider.Insert("Passport", _id, _idDriver, _lastName, _firstName, _secondName, _number, _giveOrg, _giveDate, _address, _file), out _id);

            PassportList passportList = PassportList.getInstance();
            passportList.Add(this);
        }

        internal override object[] getRow()
        {
            return new object[3] { _id, _number, _giveDate.ToShortDateString()};
        }
        
        internal override void Delete()
        {
            DeleteFile(_file);

            _provider.Delete("Passport", _id);
        }

        internal bool isEqualDriverID(Driver driver)
        {
            return driver.IsEqualsID(_idDriver);
        }

        public override string ToString()
        {
            if (_idDriver == 0)
                return "нет данных";
            else
                return string.Concat("номер ", _number, " выдан ", _giveDate.ToShortDateString());
        }
    }
}
