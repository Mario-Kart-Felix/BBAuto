using System.Data;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Dictionary;
using BBAuto.Logic.Entities;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Static;
using BBAuto.Logic.Tables;

namespace BBAuto.Logic.Common
{
  public class Employees : MainDictionary
  {
    private int idEmployeesName;
    private int idDriver;

    public string IDEmployeesName
    {
      get { return idEmployeesName.ToString(); }
      set { int.TryParse(value, out idEmployeesName); }
    }

    public string IDDriver
    {
      get { return idDriver.ToString(); }
      set { int.TryParse(value, out idDriver); }
    }

    public string Name
    {
      get
      {
        DriverList driverList = DriverList.getInstance();
        Driver driver = driverList.getItem(idDriver);

        return driver.GetName(NameType.Short);
      }
    }

    public string DriverName
    {
      get
      {
        DriverList driverList = DriverList.getInstance();
        Driver driver = driverList.getItem(idDriver);
        return driver.GetName(NameType.Full);
      }
    }

    public string EmployeeName
    {
      get
      {
        EmployeesNames employeesNames = EmployeesNames.getInstance();
        return employeesNames.getItem(idEmployeesName);
      }
    }

    public Region Region { get; set; }

    public Employees()
    {
      ID = 0;
    }

    public Employees(DataRow row)
    {
      fillFields(row);
    }

    private void fillFields(DataRow row)
    {
      int idRegion;
      int.TryParse(row.ItemArray[0].ToString(), out idRegion);
      RegionList regionList = RegionList.getInstance();
      Region = regionList.getItem(idRegion);

      int.TryParse(row.ItemArray[1].ToString(), out idEmployeesName);
      int.TryParse(row.ItemArray[2].ToString(), out idDriver);
    }

    internal override void Delete()
    {
      _provider.DoOther("exec Employees_Delete @p1, @p2", ID, idEmployeesName);
    }

    internal override object[] getRow()
    {
      return new object[5] {Region.ID, idEmployeesName, Region.Name, EmployeeName, DriverName};
    }

    public override void Save()
    {
      _provider.Insert("Employees", Region.ID, idEmployeesName, idDriver);
    }
  }
}
