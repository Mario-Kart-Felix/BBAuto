using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ClassLibraryBBAuto
{
    public class DilerList : MainList
    {
        private List<Diler> list;
        private static DilerList uniqueInstance;

        private DilerList()
        {
            list = new List<Diler>();

            loadFromSql();
        }

        public static DilerList getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new DilerList();

            return uniqueInstance;
        }

        protected override void loadFromSql()
        {
            DataTable dt = _provider.Select("Diller");

            foreach (DataRow row in dt.Rows)
            {
                Diler diller = new Diler(row);
                Add(diller);
            }
        }

        internal void Add(Diler diller)
        {
            if (list.Exists(item => item == diller))
                return;

            list.Add(diller);
        }

        public void Delete(int idDiller)
        {
            Diler diller = getItem(idDiller);

            list.Remove(diller);

            diller.Delete();
        }

        public Diler getItem(int id)
        {
            var dillers = from diller in list
                          where diller.IsEqualsID(id)
                          select diller;

            if (dillers.Count() > 0)
                return dillers.First() as Diler;
            else
                return null;
        }
        
        private DataTable createTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("Название");
            dt.Columns.Add("Контакты");

            return dt;
        }

        public DataTable ToDataTable()
        {
            DataTable dt = createTable();

            foreach (Diler diller in list)
                dt.Rows.Add(diller.getRow());

            return dt;
        }
    }
}
