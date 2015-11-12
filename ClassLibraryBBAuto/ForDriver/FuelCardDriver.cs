using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ClassLibraryBBAuto
{
    public class FuelCardDriver : MainDictionary
    {
        private int _idFuelCard;
        private int _idDriver;
        private DateTime _dateBegin;
        private DateTime _dateEnd;

        public int FuelCardID
        {
            get { return _idFuelCard; }
            set { _idFuelCard = value; }
        }

        public string DriverID
        {
            get { return _idDriver.ToString(); }
            set { int.TryParse(value, out _idDriver); }
        }

        public DateTime DateBegin
        {
            get { return _dateBegin; }
            set { _dateBegin = value; }
        }

        public bool IsNotUse
        {
            get { return _dateEnd.Year != 1; }
            set { if (!value) _dateEnd = new DateTime(1, 1, 1); }
        }
        
        public DateTime DateEnd
        {
            get { return _dateEnd; }
            set { _dateEnd = value; }
        }
        
        public Driver driver
        {
            get
            {
                DriverList driverList = DriverList.getInstance();
                return driverList.getItem(_idDriver);
            }
        }

        public FuelCard fuelCard
        {
            get
            {
                FuelCardList fuelCardList = FuelCardList.getInstance();
                return fuelCardList.getItem(_idFuelCard);
            }
        }
        
        public FuelCardDriver(int idFuelCard)
        {
            _idFuelCard = idFuelCard;
            DateBegin = DateTime.Today;
            _idDriver = 1;
            IsNotUse = false;
        }

        public FuelCardDriver(DataRow row)
        {
            fillFields(row);
        }

        private void fillFields(DataRow row)
        {
            int.TryParse(row.ItemArray[0].ToString(), out _id);
            int.TryParse(row.ItemArray[1].ToString(), out _idFuelCard);
            int.TryParse(row.ItemArray[2].ToString(), out _idDriver);
            DateTime.TryParse(row.ItemArray[3].ToString(), out _dateBegin);
            DateTime.TryParse(row.ItemArray[4].ToString(), out _dateEnd);
        }

        public override void Save()
        {
            string dateBeginSql = string.Empty;
            dateBeginSql = string.Concat(DateBegin.Year.ToString(), "-", DateBegin.Month.ToString(), "-", DateBegin.Day.ToString());

            string dateEndSql = string.Empty;
            if (_dateEnd.Year != 1)
                dateEndSql = string.Concat(_dateEnd.Year.ToString(), "-", _dateEnd.Month.ToString(), "-", _dateEnd.Day.ToString());

            int.TryParse(_provider.Insert("FuelCardDriver", _id, _idFuelCard, _idDriver, dateBeginSql, dateEndSql), out _id);

            FuelCardDriverList fuelCardDriverList = FuelCardDriverList.getInstance();
            fuelCardDriverList.Add(this);
        }

        internal override void Delete()
        {
            _provider.Delete("FuelCardDriver", _id);
        }

        internal override object[] getRow()
        {
            return new object[] { _id, _idFuelCard, fuelCard.Number, driver.GetName(NameType.Full), fuelCard.Region, fuelCard.DateEnd, fuelCard.FuelCardType, 
                DateBegin, _dateEnd };
        }

        public override string ToString()
        {
            return (fuelCard == null) ? string.Empty : fuelCard.Number + " " + fuelCard.FuelCardType;
        }
    }
}
