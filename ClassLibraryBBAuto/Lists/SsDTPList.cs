using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BBAuto.Domain.ForCar;
using BBAuto.Domain.Abstract;

namespace BBAuto.Domain.Lists
{
    public class SsDTPList : MainList
    {
        private static SsDTPList uniqueInstance;
        private List<SsDTP> list;

        private SsDTPList()
        {
            list = new List<SsDTP>();

            loadFromSql();
        }

        public static SsDTPList getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new SsDTPList();

            return uniqueInstance;
        }

        protected override void loadFromSql()
        {
            DataTable dt = _provider.Select("ssDTP");

            clearList();

            foreach (DataRow row in dt.Rows)
            {
                SsDTP ssDTP = new SsDTP(row);
                Add(ssDTP);
            }
        }

        public void Add(SsDTP ssDTP)
        {
            if (list.Exists(item => item == ssDTP))
                return;

            list.Add(ssDTP);
        }

        private void clearList()
        {
            if (list.Count > 0)
                list.Clear();
        }

        public SsDTP getItem(int idMark)
        {
            return list.FirstOrDefault(item => item.ID == idMark);
        }

        public DataTable ToDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("Марка");
            dt.Columns.Add("СТО");

            foreach (SsDTP ssDTP in list)
            {
                dt.Rows.Add(ssDTP.getRow());
            }

            return dt;
        }

        public void Delete(int idMark)
        {
            SsDTP ssDTP = getItem(idMark);

            list.Remove(ssDTP);

            ssDTP.Delete();
        }
    }
}
