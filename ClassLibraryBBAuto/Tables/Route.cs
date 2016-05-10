using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ClassLibraryBBAuto
{
    public class Route : MainDictionary
    {
        private MyPoint _myPoint1;
        private MyPoint _myPoint2;
        private int _distance;

        public MyPoint MyPoint1 { get { return _myPoint1; } }
        public MyPoint MyPoint2
        {
            get { return _myPoint2; }
            set { _myPoint2 = value; }
        }
        
        public int Distance
        {
            get { return _distance; }
            set { _distance = value; }
        }

        public Route(MyPoint myPoint1)
        {
            _myPoint1 = myPoint1;
            _distance = 0;
        }

        public Route(MyPoint myPoint1, MyPoint myPoint2, string distance)
        {
            _myPoint1 = myPoint1;
            _myPoint2 = myPoint2;
            int.TryParse(distance, out _distance);
        }

        public Route(DataRow row)
        {
            int.TryParse(row[0].ToString(), out _id);

            MyPointList myPointList = MyPointList.getInstance();
            int idMyPoint1;
            int.TryParse(row[1].ToString(), out idMyPoint1);
            _myPoint1 = myPointList.getItem(idMyPoint1);

            int idMyPoint2;
            int.TryParse(row[2].ToString(), out idMyPoint2);
            _myPoint2 = myPointList.getItem(idMyPoint2);

            int.TryParse(row[3].ToString(), out _distance);
        }

        public override void Save()
        {
            if (ID == 0)
                return;

            int.TryParse(_provider.Insert("Route", ID, _myPoint1.ID, _myPoint2.ID, _distance), out _id);

            RouteList routeList = RouteList.getInstance();
            routeList.Add(this);
        }

        internal override void Delete()
        {
            _provider.Delete("Route", ID);
        }

        internal override object[] getRow()
        {
            return new object[] { _id, _myPoint2.Name, _distance };
        }

        internal object[] getRow(MyPoint myPoint1)
        {
            MyPoint myPoint = (_myPoint1 == myPoint1) ? _myPoint2 : _myPoint1;

            return new object[] { _id, myPoint.Name, _distance };
        }
    }
}
