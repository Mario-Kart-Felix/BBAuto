using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BBAuto.Domain
{
    public class WayBillRoute : MainDictionary
    {
        private WayBillDay _wayBillDay;
        private Route _route;

        public WayBillRoute(DataRow row)
        {
            int.TryParse(row[0].ToString(), out _id);

            WayBillDayList wayBillDayList = WayBillDayList.getInstance();
            int idWayBillDay;
            int.TryParse(row[1].ToString(), out idWayBillDay);
            _wayBillDay = wayBillDayList.getItem(idWayBillDay);
            
            MyPointList myPointList = MyPointList.getInstance();
            int idMyPoint1;
            int.TryParse(row[2].ToString(), out idMyPoint1);
            MyPoint myPoint1 = myPointList.getItem(idMyPoint1);

            int idMyPoint2;
            int.TryParse(row[3].ToString(), out idMyPoint2);
            MyPoint myPoint2 = myPointList.getItem(idMyPoint2);

            string distance = row[4].ToString();

            _route = new Route(myPoint1, myPoint2, distance);
        }

        public WayBillRoute(WayBillDay wayBillDay)
        {
            _wayBillDay = wayBillDay;
        }

        public WayBillDay WayBillDay { get { return _wayBillDay; } }
        
        public Route Route
        {
            get { return _route; }
            set { _route = value; }
        }

        public override void Save()
        {
            _provider.Insert("WayBillRoute", ID, _wayBillDay.ID, _route.MyPoint1.ID, _route.MyPoint2.ID, _route.Distance);

            WayBillRouteList wayBillRouteList = WayBillRouteList.getInstance();
            wayBillRouteList.Add(this);
        }

        internal override object[] getRow()
        {
            throw new NotImplementedException();
        }
    }
}
