using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryBBAuto
{
    public class WayBillDay : IEnumerable
    {
        private Car _car;
        private DateTime _date;
        private int _count;
        private List<Route> _routes;

        public WayBillDay(Car car, DateTime date, int count)
        {
            _car = car;
            _date = date;
            _count = count;

            _routes = new List<Route>();
        }

        public string Day { get { return _date.Day.ToString(); } }
        public string Date { get { return _date.ToShortDateString(); } }

        public Driver Driver
        {
            get
            {
                DriverCarList driverCarList = DriverCarList.getInstance();

                return driverCarList.GetDriver(_car, _date);
            }
        }

        public int Distance
        {
            get
            {
                int distance = 0;
                foreach (Route route in this)
                    distance += route.Distance;

                return distance;
            }
        }

        public void Create(Random random)
        {
            SuppyAddressList suppyAddressList = SuppyAddressList.getInstance();
            SuppyAddress suppyAddress = suppyAddressList.getItemByRegion(Driver.RegionID);

            if (suppyAddress == null)
                throw new NullReferenceException("Не задан адрес подачи");

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

            } while ((residue > 20) && (_routes.Count < 16));

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
