using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ClassLibraryBBAuto
{
    public class WayBillRouteList : MainList
    {
        private List<WayBillRoute> list;
        private static WayBillRouteList uniqueInstance;

        private WayBillRouteList()
        {
            list = new List<WayBillRoute>();

            loadFromSql();
        }

        public static WayBillRouteList getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new WayBillRouteList();

            return uniqueInstance;
        }

        protected override void loadFromSql()
        {
            DataTable dt = _provider.Select("WayBillRoute");

            foreach (DataRow row in dt.Rows)
            {
                WayBillRoute wayBillRoute = new WayBillRoute(row);
                Add(wayBillRoute);
            }
        }

        public void Add(WayBillRoute wayBillRoute)
        {
            if (list.Exists(item => item == wayBillRoute))
                return;

            list.Add(wayBillRoute);
        }

        public List<Route> GetList(WayBillDay wayBillDay)
        {
            List<Route> routeList = list.Where(item => item.WayBillDay == wayBillDay).Select(item => item.Route).ToList();

            return routeList;
        }
    }
}