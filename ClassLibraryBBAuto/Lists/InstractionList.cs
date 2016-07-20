using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BBAuto.Domain
{
    public class InstractionList : MainList
    {
        private List<Instraction> list;
        private static InstractionList uniqueInstance;

        private InstractionList()
        {
            list = new List<Instraction>();

            loadFromSql();
        }

        public static InstractionList getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new InstractionList();

            return uniqueInstance;
        }

        protected override void loadFromSql()
        {
            DataTable dt = _provider.Select("Instraction");

            foreach (DataRow row in dt.Rows)
            {
                Instraction instraction = new Instraction(row);
                Add(instraction);
            }
        }

        public void Add(Instraction instraction)
        {
            if (list.Exists(item => item == instraction))
                return;

            list.Add(instraction);
        }

        public DataTable ToDataTable()
        {
            return createTable(list);
        }

        public DataTable ToDataTable(Driver driver)
        {
            var instractions = from instraction in list
                               where instraction.isEqualDriverID(driver)
                               select instraction;

            return createTable(instractions.ToList());
        }

        private DataTable createTable(List<Instraction> instractions)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("Номер");
            dt.Columns.Add("Дата инструктажа");

            foreach (Instraction instraction in instractions)
                dt.Rows.Add(instraction.getRow());

            return dt;
        }

        public Instraction getItem(int id)
        {
            var instractions = from instraction in list
                               where instraction.IsEqualsID(id)
                               select instraction;

            if (instractions.Count() > 0)
                return instractions.First() as Instraction;
            else
                return null;
        }

        public Instraction getItem(Driver driver)
        {
            var instractions = from instraction in list
                               where instraction.isEqualDriverID(driver)
                               select instraction;

            if (instractions.Count() > 0)
                return instractions.First() as Instraction;
            else
                return new Instraction(0);
        }

        public void Delete(int idInstraction)
        {
            Instraction instraction = getItem(idInstraction);

            list.Remove(instraction);

            instraction.Delete();
        }
    }
}
