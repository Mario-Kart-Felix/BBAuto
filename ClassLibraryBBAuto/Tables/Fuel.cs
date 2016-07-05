using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ClassLibraryBBAuto
{
    public class Fuel : MainDictionary
    {
        public Fuel(DataRow row)
        {
            _id = Convert.ToInt32(row[0].ToString());

            int idFuelCard;
            int.TryParse(row[1].ToString(), out idFuelCard);
            FuelCardList fuelCardList = FuelCardList.getInstance();
            FuelCard = fuelCardList.getItem(idFuelCard);
            
            Date = Convert.ToDateTime(row[2].ToString());
            Value = Convert.ToDouble(row[3].ToString());

            int idEngineType;
            int.TryParse(row[4].ToString(), out idEngineType);
            EngineTypeList engineTypeList = EngineTypeList.getInstance();
            EngineType = engineTypeList.getItem(idFuelCard);
        }

        internal Fuel(FuelCard fuelCard, DateTime date, EngineType engineType)
        {
            FuelCard = fuelCard;
            Date = date;
            EngineType = engineType;
            Value = 0;
        }

        public FuelCard FuelCard { get; private set; }
        public DateTime Date { get; private set; }
        public double Value { get; private set; }
        public EngineType EngineType { get; private set; }

        public void AddValue(double value)
        {
            Value += value;
        }

        public override void Save()
        {
            _id = Convert.ToInt32(_provider.Insert("Fuel", FuelCard.ID, Date, Value, EngineType.ID));
        }

        internal override object[] getRow()
        {
            return new object[] { ID, Date, Value };
        }
    }
}
