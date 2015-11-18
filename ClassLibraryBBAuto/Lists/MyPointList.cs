using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ClassLibraryBBAuto
{
    public class MyPointList : MainList
    {
        private List<MyPoint> list;
        private static MyPointList uniqueInstance;

        private MyPointList()
        {
            list = new List<MyPoint>();

            loadFromSql();
        }

        public static MyPointList getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new MyPointList();

            return uniqueInstance;
        }

        protected override void loadFromSql()
        {
            DataTable dt = _provider.Select("Point");

            foreach (DataRow row in dt.Rows)
            {
                MyPoint point = new MyPoint(row);
                Add(point);
            }
        }

        public void Add(MyPoint point)
        {
            if (list.Exists(item => item == point))
                return;

            list.Add(point);
        }

        public void Delete(int idPoint)
        {
            MyPoint point = getItem(idPoint);

            list.Remove(point);

            point.Delete();
        }

        public MyPoint getItem(int id)
        {
            var points = list.Where(item => item.IsEqualsID(id));

            return (points.Count() > 0) ? points.First() : new MyPoint();
        }

        public DataTable ToDataTable()
        {
            return CreateTable(list);
        }

        private DataTable CreateTable(List<MyPoint> points)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("Название");
            dt.Columns.Add("Регион");

            foreach (MyPoint point in points)
                dt.Rows.Add(point.getRow());

            return dt;
        }
    }
}
