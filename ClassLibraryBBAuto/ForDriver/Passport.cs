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
        internal DateTime giveDate;

        public string lastName;
        public string secondName;
        public string number;
        public string giveOrg;
        public string address;
        public string file;

        public string GiveDate
        {
            get
            {
                return giveDate.ToShortDateString();
            }
            set
            {
                DateTime date;                
                DateTime.TryParse(value, out date);
                giveDate = date;

                if (date.Year == 1)
                    giveDate = DateTime.Today;
            }
        }

        public Passport(int idDriver)
        {
            _id = 0;
            _idDriver = idDriver;
            giveDate = DateTime.Today;
            number = string.Empty;
        }

        public Passport(DataRow row)
        {
            fillFields(row);
        }

        private void fillFields(DataRow row)
        {
            int.TryParse(row.ItemArray[0].ToString(), out _id);
            int.TryParse(row.ItemArray[1].ToString(), out _idDriver);
            name = row.ItemArray[2].ToString();
            lastName = row.ItemArray[3].ToString();
            secondName = row.ItemArray[4].ToString();
            number = row.ItemArray[5].ToString();
            giveOrg = row.ItemArray[6].ToString();
            GiveDate = row.ItemArray[7].ToString();
            address = row.ItemArray[8].ToString();
            file = row.ItemArray[9].ToString();
            _fileBegin = file;
        }

        public override void Save()
        {
            DeleteFile(file);

            file = WorkWithFiles.fileCopyByID(file, "drivers", _idDriver, "Passports", number);

            int.TryParse(_provider.Insert("Passport", _id, _idDriver, name, lastName, secondName, number, giveOrg, giveDate, address, file), out _id);

            PassportList passportList = PassportList.getInstance();
            passportList.Add(this);
        }

        internal override object[] getRow()
        {
            return new object[3] { _id, number, giveDate.ToShortDateString()};
        }
        
        internal override void Delete()
        {
            DeleteFile(file);

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
                return string.Concat("номер ", number, " выдан ", giveDate.ToShortDateString());
        }
    }
}
