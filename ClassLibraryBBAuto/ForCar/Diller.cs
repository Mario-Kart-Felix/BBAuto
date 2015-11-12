using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ClassLibraryBBAuto
{
    public class Diler : MainDictionary, IDictionaryMVC
    {
        private string _contacts;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Text
        {
            get { return _contacts; }
            set { _contacts = value; }
        }

        public Diler()
        {
            _id = 0;
            _contacts = string.Empty;
        }

        public Diler(DataRow row)
        {
            int.TryParse(row.ItemArray[0].ToString(), out _id);
            name = row.ItemArray[1].ToString();
            _contacts = row.ItemArray[2].ToString();
        }

        public override void Save()
        {
            _provider.Insert("Diller", _id, name, _contacts);
        }

        internal override void Delete()
        {
            _provider.Delete("Diller", _id);
        }

        internal override object[] getRow()
        {
            return new object[3] { _id, name, _contacts };
        }
    }
}
