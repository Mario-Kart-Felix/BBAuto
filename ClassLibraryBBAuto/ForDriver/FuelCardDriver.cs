using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BBAuto.Domain
{
    public class FuelCardDriver : MainDictionary
    {
        public FuelCard FuelCard { get; set; }
        public DateTime DateBegin { get; set; }
        public DateTime? DateEnd { get; set; }
        public Driver Driver { get; set; }
        
        public bool IsNotUse
        {
            get { return DateEnd != null; }
            set { if (!value) DateEnd = null; }
        }
        
        public FuelCardDriver(int idFuelCard)
        {
            DateBegin = DateTime.Today;
            Driver = DriverList.getInstance().getItem(1);
            IsNotUse = false;
        }

        public FuelCardDriver(DataRow row)
        {
            fillFields(row);
        }

        private void fillFields(DataRow row)
        {
            int.TryParse(row.ItemArray[0].ToString(), out _id);

            int idFuelCard;
            int.TryParse(row.ItemArray[1].ToString(), out idFuelCard);
            FuelCard = FuelCardList.getInstance().getItem(idFuelCard);

            int idDriver;
            int.TryParse(row.ItemArray[2].ToString(), out idDriver);
            Driver = DriverList.getInstance().getItem(idDriver);

            DateTime dateBegin;
            DateTime.TryParse(row.ItemArray[3].ToString(), out dateBegin);
            DateBegin = dateBegin;

            DateTime dateEnd;
            DateTime.TryParse(row.ItemArray[4].ToString(), out dateEnd);
            DateEnd = dateEnd;
        }

        public override void Save()
        {
            string dateBeginSql = string.Empty;
            dateBeginSql = string.Concat(DateBegin.Year.ToString(), "-", DateBegin.Month.ToString(), "-", DateBegin.Day.ToString());

            string dateEndSql = string.Empty;
            if (DateEnd != null)
            {
                dateEndSql = string.Concat(DateEnd.Value.Year.ToString(), "-", DateEnd.Value.Month.ToString(), "-", DateEnd.Value.Day.ToString());
            }

            int.TryParse(_provider.Insert("FuelCardDriver", _id, (FuelCard == null) ? 0 : FuelCard.ID, Driver.ID, dateBeginSql, dateEndSql), out _id);

            FuelCardDriverList fuelCardDriverList = FuelCardDriverList.getInstance();
            fuelCardDriverList.Add(this);
        }

        internal override void Delete()
        {
            _provider.Delete("FuelCardDriver", _id);
        }

        internal override object[] getRow()
        {
            return new object[] { _id, FuelCard.ID, FuelCard.Number, Driver.GetName(NameType.Full), FuelCard.Region, FuelCard.DateEnd, FuelCard.FuelCardType, 
                DateBegin, (DateEnd == null) ? new DateTime(1, 1, 1) : DateEnd.Value };
        }

        public override string ToString()
        {
            return (FuelCard == null) ? string.Empty : FuelCard.Number + " " + FuelCard.FuelCardType;
        }
    }
}
