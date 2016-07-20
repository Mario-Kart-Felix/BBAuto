using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BBAuto.Domain
{
    public class Model : MainDictionary
    {
        private int idMark;
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

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
            _name = row.ItemArray[1].ToString();
            int.TryParse(row.ItemArray[2].ToString(), out idMark);
        }

        internal override void Delete()
        {
            _provider.Delete("Model", _id);
        }

        public override void Save()
        {
            _provider.Insert("Model", _id, _name, idMark);
        }

        internal override object[] getRow()
        {
            return new object[2] { _id, _name };
        }

        internal bool isEqualMarkID(int idMark)
        {
            return this.idMark == idMark;
        }
    }
}
