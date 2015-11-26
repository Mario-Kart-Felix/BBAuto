using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryBBAuto
{
    public class WayBillDay : IEnumerable
    {
        private Driver _driver;
        private Car _car;
        private int _day;
        private int _count;
        private List<Route> _routes;

        public WayBillDay(Car car, Driver driver, int day, int count)
        {
            _car = car;
            _driver = driver;
            _day = day;
            _count = count;

            _routes = new List<Route>();
        }

        public string Day { get { return _day.ToString(); } }

        public string Distance
        {
            get
            {
                int distance = 0;
                foreach (Route route in this)
                    distance += Convert.ToInt32(route.Distance);

                return distance.ToString();
            }
        }

        public void Create(Random random)
        {
            SuppyAddressList suppyAddressList = SuppyAddressList.getInstance();
            SuppyAddress suppyAddress = suppyAddressList.getItem(_driver.RegionID);

            MyPoint currentPoint = suppyAddress.Point;

            RouteList routeList = RouteList.getInstance();
            
            MyPointList myPointList = MyPointList.getInstance();

            int residue = _count;
            Route route;

            do
            {
                route = routeList.GetRandomItem(random, currentPoint);
                                
                _routes.Add(route);
                residue -= Convert.ToInt32(route.Distance);

                currentPoint = myPointList.getItem(route.MyPoint2ID);

            } while (residue > 10);

            if (currentPoint != suppyAddress.Point)
            {
                route = routeList.GetItem(currentPoint, suppyAddress.Point);
                _routes.Add(route);
            }
        }

        public IEnumerator GetEnumerator()
        {
            return new WayBillDayEnumerator(this);
        }

        private class WayBillDayEnumerator : IEnumerator
        {
            private WayBillDay _wayBillDay;
            private int _index;

            public WayBillDayEnumerator(WayBillDay wayBillDay)
            {
                _wayBillDay = wayBillDay;
                _index = -1;
            }

            public object Current
            {
                get { return _wayBillDay._routes[_index]; }
            }

            public bool MoveNext()
            {
                if (_index == _wayBillDay._routes.Count - 1)
                    return false;

                _index++;
                return true;
            }

            public void Reset()
            {
                _index = -1;
            }
        }
    }
}
