using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ClassLibraryBBAuto
{
    public class MyPoint : MainDictionary
    {
        private string _name;
        private int _idRegion;

        public int RegionID
        {
            get { return _idRegion; }
            set { _idRegion = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public MyPoint()
        {
            _id = 0;
            _idRegion = 0;
            _name = string.Empty;
        }

        public MyPoint(DataRow row)
        {
            int.TryParse(row[0].ToString(), out _id);
            _name = row[1].ToString();
            int.TryParse(row[2].ToString(), out _idRegion);
        }

        public override void Save()
        {
            _provider.Insert("MyPoint", _id, _name, _idRegion);

            MyPointList pointList = MyPointList.getInstance();
            pointList.Add(this);
        }

        internal override void Delete()
        {
            _provider.Delete("MyPoint", _id);
        }

        internal override object[] getRow()
        {
            Regions regions = Regions.getInstance();

            return new object[] { _id, _name, regions.getItem(_idRegion) };
        }
    }
}
