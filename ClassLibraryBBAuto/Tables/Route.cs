using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ClassLibraryBBAuto
{
    public class Route : MainDictionary
    {
        private int _idPoint1;
        private int _idPoint2;
        private int _distance;

        public Route()
        {
            _idPoint1 = 0;
            _idPoint2 = 0;
            _distance = 0;
        }

        public Route(DataRow row)
        {
            int.TryParse(row[0].ToString(), out _id);
            int.TryParse(row[1].ToString(), out _idPoint1);
            int.TryParse(row[2].ToString(), out _idPoint2);
            int.TryParse(row[3].ToString(), out _distance);
        }

        public override void Save()
        {
            base.Save();
        }

        internal override void Delete()
        {
            _provider.Delete("Route", _id);
        }

        internal override object[] getRow()
        {
            PointList pointList = PointList.getInstance();

            return new object[] { _id, pointList.getItem(_idPoint1), pointList.getItem(_idPoint2), _distance };
        }
    }
}
