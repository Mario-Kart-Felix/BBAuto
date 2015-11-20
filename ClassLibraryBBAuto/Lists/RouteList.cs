using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ClassLibraryBBAuto
{
    public class RouteList : MainList
    {
        private List<Route> list;
        private static RouteList uniqueInstance;
        
        private RouteList()
        {
            list = new List<Route>();

            loadFromSql();
        }

        public static RouteList getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new RouteList();

            return uniqueInstance;
        }

        protected override void loadFromSql()
        {
            DataTable dt = _provider.Select("Route");

            foreach (DataRow row in dt.Rows)
            {
                Route route = new Route(row);
                Add(route);
            }
        }

        public void Add(Route route)
        {
            if (list.Exists(item => item == route))
                return;

            list.Add(route);
        }

        public void Delete(int idRoute)
        {
            Route route = getItem(idRoute);

            list.Remove(route);

            route.Delete();
        }

        public Route getItem(int id)
        {
            var routes = list.Where(item => item.IsEqualsID(id));

            return (routes.Count() > 0) ? routes.First() : null;
        }

        public DataTable ToDataTable(int idMyPoint1)
        {
            var listNew = list.Where(item => (item.MyPoint1ID == idMyPoint1) || (item.MyPoint2ID == idMyPoint1)).ToList();

            return CreateTable(listNew, idMyPoint1);
        }

        private DataTable CreateTable(List<Route> routes, int idMyPoint1)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("Пункт назначения");
            dt.Columns.Add("Дистанция");

            foreach (Route route in routes)
                dt.Rows.Add(route.getRow(idMyPoint1));

            return dt;
        }

        public bool Exists(int idMyPoint1, MyPoint myPoint)
        {
            return list.Exists(item => ((item.MyPoint1ID == idMyPoint1 && myPoint.IsEqualsID(item.MyPoint2ID))) || ((item.MyPoint2ID == idMyPoint1 && myPoint.IsEqualsID(item.MyPoint1ID))));
        }

        public bool Exists(MyPoint myPoint)
        {
            return list.Exists(item => myPoint.IsEqualsID(item.MyPoint1ID) || myPoint.IsEqualsID(item.MyPoint2ID));
        }
    }
}
