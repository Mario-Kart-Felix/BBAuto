using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ClassLibraryBBAuto
{
    public class PointList : MainList
    {
        private List<Point> list;
        private static PointList uniqueInstance;

        private PointList()
        {
            list = new List<Point>();

            loadFromSql();
        }

        public static PointList getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new PointList();

            return uniqueInstance;
        }

        protected override void loadFromSql()
        {
            DataTable dt = _provider.Select("Point");

            foreach (DataRow row in dt.Rows)
            {
                Point point = new Point(row);
                Add(point);
            }
        }

        public void Add(Point point)
        {
            if (list.Exists(item => item == point))
                return;

            list.Add(point);
        }

        public void Delete(int idPoint)
        {
            Point point = getItem(idPoint);

            list.Remove(point);

            point.Delete();
        }

        public Point getItem(int id)
        {
            var points = list.Where(item => item.IsEqualsID(id));

            return (points.Count() > 0) ? points.First() : new Point();
        }

        public DataTable ToDataTable()
        {
            return CreateTable(list);
        }

        private DataTable CreateTable(List<Point> points)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("Название");
            dt.Columns.Add("Регион");

            foreach (Point point in points)
                dt.Rows.Add(point.getRow());

            return dt;
        }
    }
}
