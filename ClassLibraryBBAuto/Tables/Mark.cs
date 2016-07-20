using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BBAuto.Domain
{
    public class Mark : MainDictionary
    {
        private string _name;

        public Mark(DataRow row)
        {
            int.TryParse(row[0].ToString(), out _id);
            _name = row[1].ToString();
        }

        public string Name { get { return _name; } }

        internal override object[] getRow()
        {
            throw new NotImplementedException();
        }
    }
}
