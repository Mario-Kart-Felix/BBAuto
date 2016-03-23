using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ClassLibraryBBAuto
{
    public class TabelList : MainList
    {
        private List<Tabel> _list;
        private static TabelList _uniqueInstance;

        private TabelList()
        {
            _list = new List<Tabel>();

            loadFromSql();
        }

        public static TabelList GetInstance()
        {
            if (_uniqueInstance == null)
                _uniqueInstance = new TabelList();

            return _uniqueInstance;
        }

        protected override void loadFromSql()
        {
            DataTable dt = _provider.Select("Tabel");

            foreach (DataRow row in dt.Rows)
            {
                Tabel tabel = new Tabel(row);
                Add(tabel);
            }
        }

        public void Add(Tabel tabel)
        {
            if (_list.Exists(item => item == tabel))
                return;

            _list.Add(tabel);
        }

        internal List<int> GetDays(Driver driver, DateTime date)
        {
            var listNew = from item in _list
                          where item.driver == driver && item.Date.Year == date.Year && item.Date.Month == date.Month
                          orderby item.Date.Day
                          select item.Date.Day;

            return listNew.ToList();
        }
    }
}
