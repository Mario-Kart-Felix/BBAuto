using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ClassLibraryBBAuto
{
    public class MyPoint : MainDictionary
    {
        private int _idRegion;
        private string _name;

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
            int.TryParse(row[1].ToString(), out _idRegion);
            _name = row[2].ToString();
        }

        public override void Save()
        {
            _provider.Insert("MyPoint", _id, _idRegion, _name);

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

            return new object[] { _id, regions.getItem(_idRegion), _name };
        }
    }
}
