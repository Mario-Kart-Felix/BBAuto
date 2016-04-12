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

            if (route == null)
                return;

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

        internal Route GetRandomItem(Random random, MyPoint mainPoint)
        {
            var listNew = (from item in list
                           where mainPoint.IsEqualsID(item.MyPoint1ID) || mainPoint.IsEqualsID(item.MyPoint2ID)
                           select item).ToList();

            if (listNew.Count == 0)
                throw new NullReferenceException("Отсутствуют маршруты для данного города");

            int rand = random.Next(0, listNew.Count - 1);

            return (mainPoint.IsEqualsID(listNew[rand].MyPoint1ID)) ? new Route(listNew[rand].MyPoint1ID, listNew[rand].MyPoint2ID, listNew[rand].Distance.ToString()) : new Route(listNew[rand].MyPoint2ID, listNew[rand].MyPoint1ID, listNew[rand].Distance.ToString());
        }

        internal Route GetRandomItem(Random random, MyPoint mainPoint, MyPoint toPoint)
        {
            var listNew = (from item in list
                           where (toPoint.IsEqualsID(item.MyPoint1ID) || toPoint.IsEqualsID(item.MyPoint2ID)) && !mainPoint.IsEqualsID(item.MyPoint1ID) && !mainPoint.IsEqualsID(item.MyPoint2ID)
                           select item).ToList();

            int rand = random.Next(0, listNew.Count - 1);

            return (toPoint.IsEqualsID(listNew[rand].MyPoint1ID)) ? new Route(listNew[rand].MyPoint1ID, listNew[rand].MyPoint2ID, listNew[rand].Distance.ToString()) : new Route(listNew[rand].MyPoint2ID, listNew[rand].MyPoint1ID, listNew[rand].Distance.ToString());
        }

        internal Route GetItem(MyPoint point1, MyPoint point2)
        {
            var listNew = (from item in list
                           where (point1.IsEqualsID(item.MyPoint1ID) || point1.IsEqualsID(item.MyPoint2ID)) && (point2.IsEqualsID(item.MyPoint1ID) || point2.IsEqualsID(item.MyPoint2ID))
                           select item).ToList();

            return (point1.IsEqualsID(listNew.First().MyPoint1ID)) ? listNew.First() : new Route(listNew.First().MyPoint2ID, listNew.First().MyPoint1ID, listNew.First().Distance.ToString());
        }
    }
}
