using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ClassLibraryBBAuto
{
    public class Region : MainDictionary
    {
        private string _name;

        public Region(DataRow row)
        {
            int.TryParse(row[0].ToString(), out _id);
            _name = row[1].ToString();
        }

        public Region(string name)
        {
            _name = name;
        }

        public string Name { get { return _name; } }

        internal override object[] getRow()
        {
            throw new NotImplementedException();
        }

        public override void Save()
        {
            int.TryParse(_provider.Insert("Region", ID, Name), out _id);
        }
    }
}
