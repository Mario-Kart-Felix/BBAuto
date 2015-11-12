using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ClassLibraryBBAuto
{
    public class Model : MainDictionary
    {
        private int idMark;

        public Model(int idMark)
        {
            _id = 0;
            this.idMark = idMark;
        }

        public Model(DataRow row)
        {
            fillFields(row);
        }

        private void fillFields(DataRow row)
        {
            int.TryParse(row.ItemArray[0].ToString(), out _id);
            name = row.ItemArray[1].ToString();
            int.TryParse(row.ItemArray[2].ToString(), out idMark);
        }

        internal override void Delete()
        {
            _provider.Delete("Model", _id);
        }

        public override void Save()
        {
            _provider.Insert("Model", _id, name, idMark);
        }

        internal override object[] getRow()
        {
            return new object[2] { _id, name };
        }

        internal bool isEqualMarkID(int idMark)
        {
            return this.idMark == idMark;
        }
    }
}
