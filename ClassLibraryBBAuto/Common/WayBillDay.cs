using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ClassLibraryBBAuto
{
    public class WayBillDay : MainDictionary, IEnumerable
    {
        private Car _car;
        private Driver _driver;
        private DateTime _date;
        private int _count;
        private List<Route> _routes;

        public WayBillDay(DataRow row)
        {
            int.TryParse(row[0].ToString(), out _id);

            int idCar;
            int.TryParse(row[1].ToString(), out idCar);
            CarList carList = CarList.getInstance();
            _car = carList.getItem(idCar);

            int idDriver;
            int.TryParse(row[2].ToString(), out idDriver);
            DriverList driverList = DriverList.getInstance();
            _driver = driverList.getItem(idDriver);

            DateTime.TryParse(row[3].ToString(), out _date);
        }

        public WayBillDay(Car car, Driver driver, DateTime date, int count)
        {
            _car = car;
            _driver = driver;
            _date = date;
            _count = count;
        }
        
        public string Day { get { return _date.Day.ToString(); } }
        public DateTime Date { get { return _date; } }
        public Driver Driver { get { return _driver; } }
        public Car Car { get { return _car; } }
        
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

        public void ReadRoute(Random random)
        {
            if (_routes == null)
                GetRouteList();

            if (_routes.Count == 0)
                Create(random);
        }

        private void GetRouteList()
        {
            WayBillRouteList wayBillRouteList = WayBillRouteList.getInstance();

            _routes = wayBillRouteList.GetList(this);

            if (_routes == null)
                _routes = new List<Route>();
        }

        private void Create(Random random)
        {
            SuppyAddressList suppyAddressList = SuppyAddressList.getInstance();
            SuppyAddress suppyAddress = suppyAddressList.getItemByRegion(_driver.Region.ID);

            if (suppyAddress == null)
                throw new NullReferenceException("Не задан адрес подачи");

            MyPoint currentPoint = suppyAddress.Point;

            RouteList routeList = RouteList.getInstance();

            int residue = _count;
            Route route;

            do
            {
                int i = 0;

                do
                {
                    route = routeList.GetRandomItem(random, currentPoint);

                    if (i == 10)
                        break;
                    i++;
                }
                while (residue - route.Distance < 10);

                Add(route);
                residue -= Convert.ToInt32(route.Distance);

                currentPoint = route.MyPoint2;

            } while ((residue > 10) && (_routes.Count < 16));

            if (currentPoint != suppyAddress.Point)
            {
                route = routeList.GetItem(currentPoint, suppyAddress.Point);
                Add(route);
            }
        }

        private void Add(Route route)
        {
            _routes.Add(route);

            WayBillRoute wayBillRoute = new WayBillRoute(this);
            wayBillRoute.Route = route;
            wayBillRoute.Save();
        }

        public override void Save()
        {
            int.TryParse(_provider.Insert("WayBillDay", ID, Car.ID, Driver.ID, _date), out _id);

            WayBillDayList wayBillDayList = WayBillDayList.getInstance();
            wayBillDayList.Add(this);
        }
        
        internal override object[] getRow()
        {
            return new object[] { _date.ToShortDateString(), _driver.GetName(NameType.Short) };
        }
                
        public IEnumerator GetEnumerator()
        {
            if (_routes == null)
                GetRouteList();

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
