using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ClassLibraryBBAuto
{
    public class WayBillDaily : IEnumerable
    {
        private const int MIN_DAILY_MILEAGE = 100;

        private Car _car;
        private DateTime _date;
        private List<WayBillDay> _list;

        private MileageList _mileageList;
        
        public WayBillDaily(Car car, DateTime date)
        {
            _car = car;
            _date = date;

            _mileageList = MileageList.getInstance();

            LoadWayBillDay();

            if (_list == null)
                _list = new List<WayBillDay>();
        }

        private void LoadWayBillDay()
        {
            WayBillDayList wayBillDayList = WayBillDayList.getInstance();

            _list = wayBillDayList.getList(_car, _date);
        }
        
        public int Count { get { return _list.Count; } }

        public int BeginDistance { get { return _mileageList.GetBeginDistance(_car, _date); } }
        public int EndDistance { get { return _mileageList.GetEndDistance(_car, _date); } }
        
        public void Load()
        {
            if (_list.Count > 0)
                return;

            Dictionary<int, Driver> drivers = GetDriversDictionary();

            if (drivers.Count == 0)
                return;

            int count = _mileageList.GetDistance(_car, _date);

            Random random = new Random();

            int workDays = drivers.Count;

            bool isShortMonth = ((count / workDays) < MIN_DAILY_MILEAGE);
            if (isShortMonth)
                workDays /= 2;
            int div = random.Next(1);
            
            foreach(var item in drivers)
            {
                if ((isShortMonth) && (item.Key % 2 == div))
                    continue;

                int curCount = count - GetDistance();
                if ((workDays - _list.Count) == 0)
                    break;
                int everyDayCount = curCount / (workDays - _list.Count);

                WayBillDay wayBillDay = new WayBillDay(_car, item.Value, new DateTime(_date.Year, _date.Month, item.Key), everyDayCount);
                wayBillDay.Save();
                wayBillDay.ReadRoute(random);
                if (wayBillDay.Distance > 0)
                    _list.Add(wayBillDay);

                if (curCount < 10)
                    break;
            }
        }

        private Dictionary<int, Driver> GetDriversDictionary()
        {
            DriverCarList driverCarList = DriverCarList.getInstance();
            TabelList tabelList = TabelList.GetInstance();

            Dictionary<int, Driver> drivers = new Dictionary<int, Driver>();

            for (int day = 1; day <= _date.AddMonths(1).AddDays(-1).Day; day++)
            {
                DateTime curDate = new DateTime(_date.Year, _date.Month, day);

                Driver driver = driverCarList.GetDriver(_car, curDate);

                List<int> days = tabelList.GetDays(driver, curDate);

                if (days.Count == 0)
                    break;
                    //throw new NullReferenceException("Нет табельных листов на выбранный месяц для водителя " + driver.GetName(NameType.Short));

                if (!days.Exists(item => item == day))
                    continue;

                drivers.Add(day, driver);
            }
            
            return drivers;
        }

        private int GetDistance()
        {
            int count = 0;

            foreach (WayBillDay item in this)
                count += item.Distance;

            return count;
        }

        public DataTable ToDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Дата");
            dt.Columns.Add("Водитель");

            foreach (WayBillDay item in this)
            {
                dt.Rows.Add(item.getRow());
            }

            return dt;
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
