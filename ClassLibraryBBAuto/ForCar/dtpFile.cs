using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BBAuto.Domain
{
    public class DTPFile : MainDictionary
    {
        private int idDTP;
        private string _name;
        private string _file;

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

        public DTPFile(int idDTP)
        {
            this.idDTP = idDTP;

            _file = string.Empty;
        }

        public DTPFile(DataRow row)
        {
            fillFields(row);
        }

        private void fillFields(DataRow row)
        {
            int.TryParse(row.ItemArray[0].ToString(), out _id);
            int.TryParse(row.ItemArray[1].ToString(), out idDTP);
            _name = row.ItemArray[2].ToString();
            _file = row.ItemArray[3].ToString();
            _fileBegin = _file;
        }

        public override void Save()
        {
            if (IsNotSaved())
                int.TryParse(_provider.Insert("dtpFile", _id, idDTP, _name, _file), out _id);

            DeleteFile(_file);

            _file = WorkWithFiles.fileCopyByID(_file, "DTP", idDTP, string.Empty, _name);

            int.TryParse(_provider.Insert("dtpFile", _id, idDTP, _name, _file), out _id);
        }

        internal override object[] getRow()
        {
            return new object[3] { _id, _name, _file == string.Empty ? string.Empty : "Просмотр" };
        }

        internal override void Delete()
        {
            DeleteFile(_file);

            _provider.Delete("DtpFile", _id);
        }

        internal bool isEqualDtpID(DTP dtp)
        {
            return dtp.IsEqualsID(idDTP);
        }

        private bool IsNotSaved()
        {
            return _id == 0;
        }
    }
}
