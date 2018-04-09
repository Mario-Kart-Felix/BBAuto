using System.Collections.Generic;
using System.Data;
using System.Linq;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Tables;

namespace BBAuto.Logic.Lists
{
  public class EngineTypeList : MainList
  {
    private static EngineTypeList uniqueInstance;
    private List<EngineType> list;

    private EngineTypeList()
    {
      list = new List<EngineType>();

      loadFromSql();
    }

    public static EngineTypeList getInstance()
    {
      if (uniqueInstance == null)
        uniqueInstance = new EngineTypeList();

      return uniqueInstance;
    }

    protected override void loadFromSql()
    {
      DataTable dt = _provider.Select("EngineType");

      foreach (DataRow row in dt.Rows)
      {
        EngineType engineType = new EngineType(row);
        Add(engineType);
      }
    }

    public void Add(EngineType engineType)
    {
      if (list.Exists(et => et.ID == engineType.ID))
        return;

      list.Add(engineType);
    }

    public EngineType getItem(int id)
    {
      return list.FirstOrDefault(et => et.ID == id);
    }
  }
}
