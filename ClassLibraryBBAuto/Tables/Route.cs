using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ClassLibraryBBAuto
{
    public class Route : MainDictionary
    {
        private int _idMyPoint1;
        private int _idMyPoint2;
        private int _distance;

        public int MyPoint1ID
        {
            get { return _idMyPoint1; }
            set { _idMyPoint1 = value; }
        }

        public int MyPoint2ID
        {
            get { return _idMyPoint2; }
            set { _idMyPoint2 = value; }
        }

        public string Distance
        {
            get { return _distance.ToString(); }
            set { int.TryParse(value, out _distance); }
        }

        public Route(int idMyPoint1)
        {
            _idMyPoint1 = idMyPoint1;
            _idMyPoint2 = 0;
            _distance = 0;
        }

        public Route(int idMyPoint1, int idMyPoint2, string distance)
        {
            _idMyPoint1 = idMyPoint1;
            _idMyPoint2 = idMyPoint2;
            int.TryParse(distance, out _distance);
        }

        public Route(DataRow row)
        {
            int.TryParse(row[0].ToString(), out _id);
            int.TryParse(row[1].ToString(), out _idMyPoint1);
            int.TryParse(row[2].ToString(), out _idMyPoint2);
            int.TryParse(row[3].ToString(), out _distance);
        }

        public override void Save()
        {
            int.TryParse(_provider.Insert("Route", _id, _idMyPoint1, _idMyPoint2, _distance), out _id);

            RouteList routeList = RouteList.getInstance();
            routeList.Add(this);
        }

        internal override void Delete()
        {
            _provider.Delete("Route", _id);
        }

        internal override object[] getRow()
        {
            MyPointList pointList = MyPointList.getInstance();
            MyPoint myPoint = pointList.getItem(_idMyPoint2);

            return new object[] { _id, myPoint.Name, _distance };
        }

        internal object[] getRow(int idMyPoint1)
        {
            MyPointList pointList = MyPointList.getInstance();

            MyPoint myPoint = (_idMyPoint1 == idMyPoint1) ? pointList.getItem(_idMyPoint2) : pointList.getItem(_idMyPoint1);

            return new object[] { _id, myPoint.Name, _distance };
        }
    }
}
