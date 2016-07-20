using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BBAuto.Domain
{
    public class STS : MainDictionary
    {
        private string _number;
        private string _giveOrg;
        private DateTime _date;
        private string _file;

        public string Number
        {
            get { return _number; }
            set { _number = value.ToUpper(); }
        }

        public string GiveOrg
        {
            get { return _giveOrg; }
            set { _giveOrg = value; }
        }

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public string File
        {
            get { return _file; }
            set { _file = value; }
        }

        internal STS(int idCar)
        {
            _id = idCar;

            Date = DateTime.Today;
        }

        public STS(DataRow row)
        {
            int.TryParse(row.ItemArray[0].ToString(), out _id);
            Number = row.ItemArray[1].ToString();
            Date = Convert.ToDateTime(row.ItemArray[2]);
            GiveOrg = row.ItemArray[3].ToString();
            File = row.ItemArray[4].ToString();
            _fileBegin = File;
        }

        public override void Save()
        {
            DeleteFile(File);

            File = WorkWithFiles.fileCopyByID(File, "cars", _id, "", "STS");

            _provider.Insert("STS", _id, Number, Date, GiveOrg, File);

            STSList stsList = STSList.getInstance();
            stsList.Add(this);
        }

        internal override object[] getRow()
        {
            return null;
        }

        internal override void Delete()
        {
            DeleteFile(File);

            _provider.Delete("STS", _id);
        }

        internal bool isEqualCarID(Car car)
        {
            return car.IsEqualsID(_id);
        }
    }
}
