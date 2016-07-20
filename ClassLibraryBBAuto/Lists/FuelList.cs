using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BBAuto.Domain
{
    public class FuelList : MainList
    {
        private List<Fuel> list;
        private static FuelList uniqueInstance;

        private FuelList()
        {
            list = new List<Fuel>();

            loadFromSql();
        }

        public static FuelList getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new FuelList();

            return uniqueInstance;
        }

        protected override void loadFromSql()
        {
            DataTable dt = _provider.Select("Fuel");

            foreach (DataRow row in dt.Rows)
            {
                Fuel fuel = new Fuel(row);
                Add(fuel);
            }
        }

        public void Add(Fuel fuel)
        {
            if (list.Exists(item => item == fuel))
                return;

            list.Add(fuel);
        }

        public Fuel getItem(int id)
        {
            var fuels = list.Where(item => item.IsEqualsID(id));

            return (fuels.Count() > 0) ? fuels.First() : null;
        }

        public Fuel getItem(FuelCard fuelCard, DateTime date, EngineType engineType)
        {
            var fuels = list.Where(item => item.FuelCard == fuelCard && item.Date == date && item.EngineType == engineType);

            if (fuels.Count() > 0)
                return fuels.First();

            Fuel fuel = new Fuel(fuelCard, date, engineType);
            Add(fuel);

            return fuel;
        }
                
        public DataTable ToDataTable(Car car, DateTime date)
        {
            var listFiltred = GetListFiltred(car, date);

            return CreateTable(listFiltred);
        }
        
        public List<Fuel> GetListFiltred(Car car, DateTime date)
        {
            var dt = _provider.DoOther("exec FuelByCarAndDate_Select @p1, @p2", car.ID, date);

            var listFiltred = new List<Fuel>();

            foreach (DataRow row in dt.Rows)
            {
                listFiltred.Add(getItem(Convert.ToInt32(row.ItemArray[0])));
            }

            return listFiltred;
        }
                
        private DataTable CreateTable(List<Fuel> listNew)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("Дата", Type.GetType("System.DateTime"));
            dt.Columns.Add("Объём");

            foreach (var item in listNew)
            {
                dt.Rows.Add(item.getRow());
            }

            return dt;
        }
    }
}
