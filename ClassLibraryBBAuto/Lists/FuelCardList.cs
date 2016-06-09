using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ClassLibraryBBAuto
{
    public class FuelCardList : MainList
    {
        private static FuelCardList _uniqueInstance;
        private List<FuelCard> _list;

        private FuelCardList()
        {
            _list = new List<FuelCard>();

            loadFromSql();
        }

        public static FuelCardList getInstance()
        {
            if (_uniqueInstance == null)
                _uniqueInstance = new FuelCardList();

            return _uniqueInstance;
        }

        protected override void loadFromSql()
        {
            DataTable dt = _provider.Select("FuelCard");

            _list.Clear();

            foreach (DataRow row in dt.Rows)
            {
                FuelCard fuelCard = new FuelCard(row);
                Add(fuelCard);
            }
        }

        internal void Add(FuelCard fuelCard)
        {
            if (_list.Exists(item => item == fuelCard))
                return;

            _list.Add(fuelCard);
        }

        public FuelCard getItem(int idFuelCard)
        {
            List<FuelCard> list = _list.Where(item => item.IsEqualsID(idFuelCard)).ToList();

            return (list.Count == 0) ? null : list.First();
        }

        public FuelCard getItem(string number)
        {
            List<FuelCard> list = _list.Where(item => item.Number == number).ToList();

            return (list.Count == 0) ? null : list.First();
        }

        public void Delete(int idFuelCard)
        {
            FuelCard fuelCard = getItem(idFuelCard);

            _list.Remove(fuelCard);

            fuelCard.Delete();
        }
        
        public DataTable ToDataTable()
        {
            return createTable(_list.OrderBy(item => item.IsLost).ThenBy(item => item.DateEnd).ToList());
        }
        
        private DataTable createTable(List<FuelCard> list)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("idFuelCardDriver");
            dt.Columns.Add("idFuelCard");
            dt.Columns.Add("Номер");
            dt.Columns.Add("Водитель");
            dt.Columns.Add("Регион");
            dt.Columns.Add("Срок действия", Type.GetType("System.DateTime"));
            dt.Columns.Add("Фирма");
            dt.Columns.Add("Начало использования", Type.GetType("System.DateTime"));
            dt.Columns.Add("Окончание использования", Type.GetType("System.DateTime"));

            foreach (FuelCard fuelCard in list)
                dt.Rows.Add(fuelCard.getRow());

            return dt;
        }
    }
}
