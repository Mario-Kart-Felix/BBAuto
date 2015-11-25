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

        public MyPoint Point
        {
            get
            {
                MyPointList myPointList = MyPointList.getInstance();
                return myPointList.getItem(_id);
            }
        }

        public string Region
        {
            get
            {
                Regions regions = Regions.getInstance();
                return regions.getItem(Point.RegionID);
            }
        }
        
        public SuppyAddress()
        {
            _id = 0;
        }

        public SuppyAddress(DataRow row)
        {
            int.TryParse(row.ItemArray[0].ToString(), out _id);
        }

        internal override void Delete()
        {
            _provider.Delete("SuppyAddress", _id);
        }

        internal override object[] getRow()
        {
            return new object[] { _id, Region, Point.Name};
        }

        public override void Save()
        {
            _provider.Insert("SuppyAddress", _id);

            SuppyAddressList suppyAddressList = SuppyAddressList.getInstance();
            suppyAddressList.Add(this);
        }

        public override string ToString()
        {
            return string.Concat("г. ", Region, " ", Point.Name);
        }
    }
}
