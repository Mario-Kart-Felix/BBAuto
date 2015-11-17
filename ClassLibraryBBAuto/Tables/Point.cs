using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ClassLibraryBBAuto
{
    public class Point : MainDictionary
    {
        private string _name;
        private int _idRegion;

        public Point()
        {
            _id = 0;
            _idRegion = 0;
            _name = string.Empty;
        }

        public Point(DataRow row)
        {
            int.TryParse(row[0].ToString(), out _id);
            _name = row[1].ToString();
            int.TryParse(row[2].ToString(), out _idRegion);
        }

        public override void Save()
        {
            _provider.Insert("Point", _id, _name, _idRegion);

            PointList pointList = PointList.getInstance();
            pointList.Add(this);
        }

        internal override object[] getRow()
        {
            Regions regions = Regions.getInstance();

            return new object[] { _id, _name, regions.getItem(_idRegion) };
        }
    }
}
