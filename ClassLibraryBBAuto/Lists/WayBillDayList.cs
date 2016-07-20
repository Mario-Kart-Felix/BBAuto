using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BBAuto.Domain
{
    public class WayBillDayList : MainList
    {
        private List<WayBillDay> list;
        private static WayBillDayList uniqueInstance;

        private WayBillDayList()
        {
            list = new List<WayBillDay>();

            loadFromSql();
        }

        public static WayBillDayList getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new WayBillDayList();

            return uniqueInstance;
        }

        protected override void loadFromSql()
        {
            DataTable dt = _provider.Select("WayBillDay");

            foreach (DataRow row in dt.Rows)
            {
                WayBillDay wayBillDay = new WayBillDay(row);
                Add(wayBillDay);
            }
        }

        public void Add(WayBillDay wayBillDay)
        {
            if (list.Exists(item => item == wayBillDay))
                return;

            list.Add(wayBillDay);
        }

        public WayBillDay getItem(int id)
        {
            var wayBillDays = list.Where(item => item.IsEqualsID(id));

            return (wayBillDays.Count() > 0) ? wayBillDays.First() : null;
        }

        public List<WayBillDay> getList(Car car, DateTime date)
        {
            return list.Where(item => item.Car == car && item.Date.Year == date.Year && item.Date.Month == date.Month).OrderBy(item => item.Date).ToList();
        }
    }
}
