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
            DataTable dt = _provider.Select("MyPoint");

            foreach (DataRow row in dt.Rows)
            {
                MyPoint myPoint = new MyPoint(row);
                Add(myPoint);
            }
        }

        public void Add(MyPoint myPoint)
        {
            if (list.Exists(item => item == myPoint))
                return;

            list.Add(myPoint);
        }

        public void Delete(int idMyPoint)
        {
            MyPoint myPoint = getItem(idMyPoint);

            list.Remove(myPoint);

            myPoint.Delete();
        }

        public MyPoint getItem(int id)
        {
            var myPoints = list.Where(item => item.IsEqualsID(id));

            return (myPoints.Count() > 0) ? myPoints.First() : new MyPoint();
        }

        public DataTable ToDataTable()
        {
            return CreateTable(list);
        }

        private DataTable CreateTable(List<MyPoint> myPoints)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("Регион");
            dt.Columns.Add("Название");

            foreach (MyPoint myPoint in myPoints)
                dt.Rows.Add(myPoint.getRow());

            return dt;
        }
    }
}
