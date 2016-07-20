using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BBAuto.Domain
{
    public class Culprits : MyDictionary
    {
        private static Culprits uniqueInstance;

        public static Culprits getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new Culprits();

            return uniqueInstance;
        }

        protected override void loadFromSql()
        {
            DataTable dt = provider.Select("Culprit");

            fillList(dt);
        }

        public DataTable ToDataTable(DTP dtp)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("Название");

            foreach (var item in dictionary)
                dt.Rows.Add(new object[2] { item.Key, item.Value });

            dt.Rows.Add(dtp.getCulpit());

            return dt;
        }
    }
}
