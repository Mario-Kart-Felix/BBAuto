using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ClassLibraryBBAuto
{
    public class DTPFile : MainDictionary
    {
        private int idDTP;
        public string file;

        public DTPFile(int idDTP)
        {
            this.idDTP = idDTP;

            file = string.Empty;
        }

        public DTPFile(DataRow row)
        {
            fillFields(row);
        }

        private void fillFields(DataRow row)
        {
            int.TryParse(row.ItemArray[0].ToString(), out _id);
            int.TryParse(row.ItemArray[1].ToString(), out idDTP);
            name = row.ItemArray[2].ToString();
            file = row.ItemArray[3].ToString();
            _fileBegin = file;
        }

        public override void Save()
        {
            if (IsNotSaved())
                int.TryParse(_provider.Insert("dtpFile", _id, idDTP, name, file), out _id);

            DeleteFile(file);

            file = WorkWithFiles.fileCopyByID(file, "DTP", idDTP, string.Empty, name);

            int.TryParse(_provider.Insert("dtpFile", _id, idDTP, name, file), out _id);
        }

        internal override object[] getRow()
        {
            return new object[3] { _id, name, file == string.Empty ? string.Empty : "Просмотр" };
        }

        internal override void Delete()
        {
            DeleteFile(file);

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
