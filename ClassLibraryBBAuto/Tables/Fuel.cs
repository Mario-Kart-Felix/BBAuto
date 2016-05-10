using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ClassLibraryBBAuto
{
    public class Fuel : MainDictionary
    {
        private FuelCard _fuelCard;
        private DateTime _date;
        private double _count;
        private EngineType _engineType;

        public Fuel(DataRow row)
        {
            int.TryParse(row[0].ToString(), out _id);

            int idFuelCard;
            int.TryParse(row[1].ToString(), out idFuelCard);
            FuelCardList fuelCardList = FuelCardList.getInstance();
            _fuelCard = fuelCardList.getItem(idFuelCard);

            DateTime.TryParse(row[2].ToString(), out _date);

            int idEngineType;
            int.TryParse(row[3].ToString(), out idEngineType);
            EngineTypeList engineTypeList = EngineTypeList.getInstance();
            _engineType = engineTypeList.getItem(idFuelCard);
        }

        internal Fuel(FuelCard fuelCard, DateTime date, EngineType engineType)
        {
            _fuelCard = fuelCard;
            _date = date;
            _engineType = engineType;
            _count = 0;
        }

        public FuelCard FuelCard { get { return _fuelCard; } }
        public DateTime Date { get { return _date; } }
        public double Count { get { return _count; } }
        public EngineType EngineType { get { return _engineType; } }

        public void AddCount(double count)
        {
            _count += count;
        }

        internal override object[] getRow()
        {
            throw new NotImplementedException();
        }
    }
}
