using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BBAuto.Domain
{
    public class Instraction : MainDictionary
    {
        private int _idDriver;
        private DateTime date;
        private string _file;
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string File
        {
            get { return _file; }
            set { _file = value; }
        }

        public string Date
        {
            get { return date.ToShortDateString(); }
            set
            {
                if (!DateTime.TryParse(value, out date))
                    date = DateTime.Today.Date;
            }
        }

        public Instraction(int idDriver)
        {
            _idDriver = idDriver;
            Date = "";
            _id = 0;
        }

        public Instraction(DataRow row)
        {
            fillFields(row);
        }

        private void fillFields(DataRow row)
        {
            int.TryParse(row.ItemArray[0].ToString(), out _id);
            _name = row.ItemArray[1].ToString();
            Date = row.ItemArray[2].ToString();
            int.TryParse(row.ItemArray[3].ToString(), out _idDriver);
            _file = row.ItemArray[4].ToString();
            _fileBegin = _file;
        }

        internal override void Delete()
        {
            DeleteFile(_file);

            _provider.Delete("Instraction", _id);
        }

        public override void Save()
        {
            DeleteFile(_file);

            _file = WorkWithFiles.fileCopyByID(_file, "drivers", _idDriver, "Instraction", _name);

            int.TryParse(_provider.Insert("Instraction", _id, _idDriver, _name, date, _file), out _id);

            InstractionList instractionList = InstractionList.getInstance();
            instractionList.Add(this);
        }

        internal override object[] getRow()
        {
            return new object[3] { _id, _name, Date };
        }

        internal bool isEqualDriverID(Driver driver)
        {
            return driver.IsEqualsID(_idDriver);
        }

        public override string ToString()
        {
            return (_idDriver == 0) ? "нет данных" : string.Concat("№", _name, " дата ", Date);
        }
    }
}
