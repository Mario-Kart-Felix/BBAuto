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
    }
}
