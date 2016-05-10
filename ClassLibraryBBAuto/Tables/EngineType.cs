using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ClassLibraryBBAuto
{
    public class EngineType : MainDictionary
    {
        private string _name;
        private string _shortName;

        public EngineType(DataRow row)
        {
            int.TryParse(row[0].ToString(), out _id);

            _name = row[1].ToString();
            _shortName = row[2].ToString();
        }

        internal override object[] getRow()
        {
            throw new NotImplementedException();
        }

        public string Name { get { return _name; } }
        public string ShortName { get { return _shortName; } }
    }
}
