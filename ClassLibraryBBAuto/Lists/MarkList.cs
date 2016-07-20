using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BBAuto.Domain
{
    public class MarkList : MainList
    {
        private List<Mark> list;
        private static MarkList uniqueInstance;

        private MarkList()
        {
            list = new List<Mark>();

            loadFromSql();
        }

        public static MarkList getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new MarkList();

            return uniqueInstance;
        }

        protected override void loadFromSql()
        {
            DataTable dt = _provider.Select("Mark");

            foreach (DataRow row in dt.Rows)
            {
                Mark mark = new Mark(row);
                Add(mark);
            }
        }

        public void Add(Mark mark)
        {
            if (list.Exists(item => item == mark))
                return;

            list.Add(mark);
        }
        
        public Mark getItem(int id)
        {
            var marks = list.Where(item => item.IsEqualsID(id));

            return (marks.Count() > 0) ? marks.First() : null;
        }
    }
}
