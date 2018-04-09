using System.Collections.Generic;
using System.Data;
using System.Linq;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Entities;
using BBAuto.Logic.ForDriver;

namespace BBAuto.Logic.Lists
{
  public class InstractionList : MainList
  {
    private static InstractionList uniqueInstance;
    private List<Instraction> list;

    private InstractionList()
    {
      list = new List<Instraction>();

      loadFromSql();
    }

    public static InstractionList getInstance()
    {
      if (uniqueInstance == null)
        uniqueInstance = new InstractionList();

      return uniqueInstance;
    }

    protected override void loadFromSql()
    {
      DataTable dt = _provider.Select("Instraction");

      foreach (DataRow row in dt.Rows)
      {
        Instraction instraction = new Instraction(row);
        Add(instraction);
      }
    }

    public void Add(Instraction instraction)
    {
      if (list.Exists(item => item == instraction))
        return;

      list.Add(instraction);
    }

    public DataTable ToDataTable()
    {
      return CreateTable(list);
    }

    public DataTable ToDataTable(Driver driver)
    {
      return CreateTable(list.Where(i => i.Driver.ID == driver.ID));
    }

    private DataTable CreateTable(IEnumerable<Instraction> instractions)
    {
      DataTable dt = new DataTable();
      dt.Columns.Add("id");
      dt.Columns.Add("Номер");
      dt.Columns.Add("Дата инструктажа");

      foreach (Instraction instraction in instractions)
        dt.Rows.Add(instraction.getRow());

      return dt;
    }

    public Instraction getItem(int id)
    {
      return list.FirstOrDefault(i => i.ID == id);
    }

    public Instraction getItem(Driver driver)
    {
      return list.FirstOrDefault(i => i.Driver.ID == driver.ID);
    }

    public void Delete(int idInstraction)
    {
      Instraction instraction = getItem(idInstraction);

      list.Remove(instraction);

      instraction.Delete();
    }
  }
}
