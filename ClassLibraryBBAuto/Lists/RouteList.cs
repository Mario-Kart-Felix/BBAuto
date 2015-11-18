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

            return (routes.Count() > 0) ? routes.First() : new Route();
        }

        public DataTable ToDataTable()
        {
            return CreateTable(list);
        }

        private DataTable CreateTable(List<Route> routes)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("Пункт 1");
            dt.Columns.Add("Пункт 2");
            dt.Columns.Add("Дистанция");

            foreach (Route route in routes)
                dt.Rows.Add(route.getRow());

            return dt;
        }
    }
}
