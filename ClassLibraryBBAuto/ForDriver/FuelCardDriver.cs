using System;
using System.Data;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Entities;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Static;

namespace BBAuto.Logic.ForDriver
{
  public class FuelCardDriver : MainDictionary
  {
    public FuelCard FuelCard { get; private set; }
    public DateTime DateBegin { get; set; }
    public DateTime? DateEnd { get; set; }
    public Driver Driver { get; set; }

    public bool IsNotUse
    {
      get => DateEnd != null;
      set
      {
        if (!value) DateEnd = null;
      }
    }

    public FuelCardDriver(FuelCard fuelCard)
    {
      FuelCard = fuelCard;
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
      ID = Convert.ToInt32(row.ItemArray[0]);

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
      if (DateTime.TryParse(row.ItemArray[4].ToString(), out dateEnd))
      {
        DateEnd = dateEnd;
      }
    }

    public override void Save()
    {
      string dateBeginSql = string.Empty;
      dateBeginSql = string.Concat(DateBegin.Year.ToString(), "-", DateBegin.Month.ToString(), "-",
        DateBegin.Day.ToString());

      string dateEndSql = string.Empty;
      if (DateEnd != null)
      {
        dateEndSql = string.Concat(DateEnd.Value.Year.ToString(), "-", DateEnd.Value.Month.ToString(), "-",
          DateEnd.Value.Day.ToString());
      }

      ID = Convert.ToInt32(_provider.Insert("FuelCardDriver", ID, (FuelCard == null) ? 0 : FuelCard.ID, Driver.ID,
        dateBeginSql, dateEndSql));

      FuelCardDriverList fuelCardDriverList = FuelCardDriverList.getInstance();
      fuelCardDriverList.Add(this);
    }

    internal override void Delete()
    {
      _provider.Delete("FuelCardDriver", ID);
    }

    internal override object[] getRow()
    {
      return new object[]
      {
        ID, FuelCard.ID, FuelCard.Number, Driver.GetName(NameType.Full), FuelCard.Region, FuelCard.DateEnd,
        FuelCard.FuelCardType,
        DateBegin, (DateEnd == null) ? new DateTime(1, 1, 1) : DateEnd.Value
      };
    }

    public override string ToString()
    {
      return (FuelCard == null) ? string.Empty : FuelCard.Number + " " + FuelCard.FuelCardType;
    }
  }
}
