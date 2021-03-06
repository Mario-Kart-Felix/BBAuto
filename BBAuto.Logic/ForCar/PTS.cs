using System;
using System.Data;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Common;
using BBAuto.Logic.Entities;
using BBAuto.Logic.Lists;

namespace BBAuto.Logic.ForCar
{
  public class PTS : MainDictionary
  {
    private string _number;

    public string Number
    {
      get => _number;
      set => _number = value.ToUpper();
    }

    public string GiveOrg { get; set; }
    public DateTime Date { get; set; }
    public string File { get; set; }

    public Car Car { get; private set; }

    internal PTS(Car car)
    {
      Car = car;
      Date = DateTime.Today;
      Number = string.Empty;
    }

    public PTS(DataRow row)
    {
      int idCar;
      int.TryParse(row.ItemArray[0].ToString(), out idCar);
      Car = CarList.getInstance().getItem(idCar);

      Number = row.ItemArray[1].ToString();
      Date = Convert.ToDateTime(row.ItemArray[2]);
      GiveOrg = row.ItemArray[3].ToString();
      File = row.ItemArray[4].ToString();
      FileBegin = File;
    }

    public override void Save()
    {
      DeleteFile(File);

      File = WorkWithFiles.FileCopyById(File, "cars", Car.Id, "", "PTS");

      Provider.Insert("PTS", Car.Id, Number, Date, GiveOrg, File);

      PTSList ptsList = PTSList.getInstance();
      ptsList.Add(this);
    }

    internal override void Delete()
    {
      DeleteFile(File);

      Provider.Delete("PTS", Car.Id);
    }

    internal override object[] GetRow()
    {
      return null;
    }
  }
}
