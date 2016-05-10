using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ClassLibraryBBAuto
{
    public class SuppyAddress : MainDictionary
    {
        private MyPoint _point;

        public MyPoint Point
        {
            get { return _point; }
            set { _point = value; }
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
            int idPoint;
            MyPointList myPointList = MyPointList.getInstance();
            int.TryParse(row.ItemArray[0].ToString(), out idPoint);
            Point = myPointList.getItem(idPoint);
        }

        internal override void Delete()
        {
            _provider.Delete("SuppyAddress", _id);
        }

        internal override object[] getRow()
        {
            return new object[] { _point.ID, Region, Point.Name };
        }

        public override void Save()
        {
            _provider.Insert("SuppyAddress", _point.ID);

            SuppyAddressList suppyAddressList = SuppyAddressList.getInstance();
            suppyAddressList.Add(this);
        }

        public override string ToString()
        {
            return string.Concat("г. ", Region, " ", Point.Name);
        }

        public bool IsEqualsRegionID(int idRegion)
        {
            return ((Point != null) && (Point.RegionID == idRegion));
        }
    }
}
