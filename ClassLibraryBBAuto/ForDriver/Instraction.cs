using BBAuto.Domain.Abstract;
using BBAuto.Domain.Common;
using BBAuto.Domain.Entities;
using BBAuto.Domain.Lists;
using System;
using System.Data;

namespace BBAuto.Domain.ForDriver
{
  public class Instraction : MainDictionary
  {
    private DateTime date;

    public string Name { get; set; }
    public string File { get; set; }
    public Driver Driver { get; private set; }

    public string Date
    {
      get { return date.ToShortDateString(); }
      set
      {
        if (!DateTime.TryParse(value, out date))
          date = DateTime.Today.Date;
      }
    }

    public Instraction(Driver driver)
    {
      Driver = driver;
      Date = "";
      ID = 0;
    }

    public Instraction(DataRow row)
    {
      fillFields(row);
    }

    private void fillFields(DataRow row)
    {
      ID = Convert.ToInt32(row.ItemArray[0]);
      Name = row.ItemArray[1].ToString();
      Date = row.ItemArray[2].ToString();

      int idDriver;
      int.TryParse(row.ItemArray[3].ToString(), out idDriver);
      Driver = DriverList.getInstance().getItem(idDriver);

      File = row.ItemArray[4].ToString();
      _fileBegin = File;
    }

    internal override void Delete()
    {
      DeleteFile(File);

      _provider.Delete("Instraction", ID);
    }

    public override void Save()
    {
      DeleteFile(File);

      File = WorkWithFiles.FileCopyById(File, "drivers", Driver.ID, "Instraction", Name);

      ID = Convert.ToInt32(_provider.Insert("Instraction", ID, Driver.ID, Name, date, File));

      InstractionList instractionList = InstractionList.getInstance();
      instractionList.Add(this);
    }

    internal override object[] getRow()
    {
      return new object[] {ID, Name, Date};
    }

    public override string ToString()
    {
      return (Driver == null) ? "нет данных" : string.Concat("№", Name, " дата ", Date);
    }
  }
}
