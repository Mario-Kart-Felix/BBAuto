using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryBBAuto
{
    public class WayBillDaily : IEnumerable
    {
        private Driver _driver;
        private Car _car;
        private DateTime _date;
        private List<WayBillDay> _list;

        public WayBillDaily(Car car, DateTime date)
        {
            _car = car;
            _date = date;

            DriverCarList driverCarList = DriverCarList.getInstance();

            _driver = driverCarList.GetDriver(_car);
            _list = new List<WayBillDay>();
        }

        public void Create()
        {
            TabelList tabelList = TabelList.GetInstance();

            List<int> days = tabelList.GetDays(_driver, _date);

            if (days.Count == 0)
                throw new NullReferenceException("Водитель не работал в выбранном месяце");

            MileageList mileageList = MileageList.getInstance();
            int count = 1000;//mileageList.GetDistance(_car, _date);
            int everyDayCount = count / days.Count;

            foreach (var day in days)
            {
                WayBillDay wayBillDay = new WayBillDay(_car, _driver, day, everyDayCount);
                wayBillDay.Create();
                _list.Add(wayBillDay);
            }
        }

        public IEnumerator GetEnumerator()
        {
            return new WayBillEnumerator(this);
        }

        private class WayBillEnumerator : IEnumerator
        {
            private int _index = -1;
            private WayBillDaily _wayBillDaily;

            public WayBillEnumerator(WayBillDaily wayBillDaily)
            {
                _wayBillDaily = wayBillDaily;
            }

            public object Current
            {
                get { return _wayBillDaily._list[_index]; }
            }

            public bool MoveNext()
            {
                if (_index == _wayBillDaily._list.Count - 1)
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
