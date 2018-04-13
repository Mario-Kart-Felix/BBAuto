using System.Collections.Generic;
using System.Data;
using System.Linq;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.ForCar;

namespace BBAuto.Logic.Lists
{
  public class DealerList : MainList
  {
    private static DealerList uniqueInstance;
    private List<Dealer> list;

    private DealerList()
    {
      list = new List<Dealer>();

      LoadFromSql();
    }

    public static DealerList getInstance()
    {
      if (uniqueInstance == null)
        uniqueInstance = new DealerList();

      return uniqueInstance;
    }

    protected override void LoadFromSql()
    {
      DataTable dt = Provider.Select("Diller");

      foreach (DataRow row in dt.Rows)
      {
        Dealer diller = new Dealer(row);
        Add(diller);
      }
    }

    internal void Add(Dealer diller)
    {
      if (list.Exists(item => item == diller))
        return;

      list.Add(diller);
    }

    public void Delete(int idDiller)
    {
      Dealer diller = getItem(idDiller);

      list.Remove(diller);

      diller.Delete();
    }

    public Dealer getItem(int id)
    {
      return list.FirstOrDefault(d => d.Id == id);
    }

    private DataTable createTable()
    {
      DataTable dt = new DataTable();
      dt.Columns.Add("id");
      dt.Columns.Add("Название");
      dt.Columns.Add("Контакты");

      return dt;
    }

    public DataTable ToDataTable()
    {
      DataTable dt = createTable();

      foreach (Dealer diller in list)
        dt.Rows.Add(diller.GetRow());

      return dt;
    }
  }
}
