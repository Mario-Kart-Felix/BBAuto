using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ClassLibraryBBAuto
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
            FuelCardDriverList fuelCardDriverList = FuelCardDriverList.getInstance();

            DateTime datelastDay = new DateTime(date.Year, date.Month, 1).AddMonths(1).AddDays(-1);

            var listFiltred = new List<Fuel>();

            for (date = new DateTime(date.Year, date.Month, 1); date.Day <= datelastDay.Day && date.Month == datelastDay.Month; date = date.AddDays(1))
            {
                var listNew = list.Where(item => 
                    {
                        var fuelCardDriver = fuelCardDriverList.getItem(car, date);
                        return (fuelCardDriver != null) ? fuelCardDriver.FuelCard == item.FuelCard && item.Date == date : false;
                    }).ToList();

                if (listNew.Count > 0)
                {
                    listFiltred.Add(listNew.First());
                }
            }
            
            return CreateTable(listFiltred);
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
