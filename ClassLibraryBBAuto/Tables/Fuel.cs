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
            Count = Convert.ToDouble(row[3].ToString());

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
            Count = 0;
        }

        public FuelCard FuelCard { get; private set; }
        public DateTime Date { get; private set; }
        public double Count { get; private set; }
        public EngineType EngineType { get; private set; }

        public void AddCount(double count)
        {
            Count += count;
        }

        public override void Save()
        {
            _id = Convert.ToInt32(_provider.Insert("Fuel", FuelCard.ID, Date, Count, EngineType.ID));
        }

        internal override object[] getRow()
        {
            throw new NotImplementedException();
        }
    }
}
