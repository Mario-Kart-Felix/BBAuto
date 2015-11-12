using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ClassLibraryBBAuto
{
    public class Instraction : MainDictionary
    {
        private int idDriver;
        private DateTime date;

        public string file;

        public string Date
        {
            get
            {
                return date.ToShortDateString();
            }
            set
            {
                if (!DateTime.TryParse(value, out date))
                    date = DateTime.Today.Date;
            }
        }

        public Instraction(int idDriver)
        {
            this.idDriver = idDriver;
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
            name = row.ItemArray[1].ToString();
            Date = row.ItemArray[2].ToString();
            int.TryParse(row.ItemArray[3].ToString(), out idDriver);
            file = row.ItemArray[4].ToString();
            _fileBegin = file;
        }

        internal override void Delete()
        {
            DeleteFile(file);

            _provider.Delete("Instraction", _id);
        }

        public override void Save()
        {
            DeleteFile(file);

            file = WorkWithFiles.fileCopyByID(file, "drivers", idDriver, "Instraction", name);

            int.TryParse(_provider.Insert("Instraction", _id, idDriver, name, date, file), out _id);

            InstractionList instractionList = InstractionList.getInstance();
            instractionList.Add(this);
        }

        internal override object[] getRow()
        {
            return new object[3] { _id, name, Date };
        }

        internal bool isEqualDriverID(Driver driver)
        {
            return driver.IsEqualsID(idDriver);
        }

        public override string ToString()
        {
            return (idDriver == 0) ? "нет данных" : string.Concat("№", name, " дата ", Date);
        }
    }
}
