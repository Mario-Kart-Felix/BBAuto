using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ClassLibraryBBAuto
{
    public class SuppyAddress : MainDictionary
    {
        public string ID
        {
            get { return _id.ToString(); }
            set { int.TryParse(value, out _id); }
        }

        public string Region
        {
            get
            {
                Regions regions = Regions.getInstance();
                return regions.getItem(_id);
            }
        }

        public SuppyAddress()
        {
            _id = 0;
        }

        public SuppyAddress(DataRow row)
        {
            fillFields(row);
        }

        private void fillFields(DataRow row)
        {
            int.TryParse(row.ItemArray[0].ToString(), out _id);
            name = row.ItemArray[1].ToString();
        }

        internal override void Delete()
        {
            _provider.Delete("SuppyAddress", _id);
        }

        internal override object[] getRow()
        {
            return new object[] { _id, Region, name};
        }

        public override void Save()
        {
            _provider.Insert("SuppyAddress", _id, name);
        }
    }
}
