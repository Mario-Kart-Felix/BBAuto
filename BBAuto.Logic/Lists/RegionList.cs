using System.Collections.Generic;
using System.Data;
using System.Linq;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Tables;

namespace BBAuto.Logic.Lists
{
  public class RegionList : MainList
  {
    private List<Region> list;
    private static RegionList uniqueInstance;

    private RegionList()
    {
      list = new List<Region>();

      LoadFromSql();
    }

    public static RegionList getInstance()
    {
      if (uniqueInstance == null)
        uniqueInstance = new RegionList();

      return uniqueInstance;
    }

    protected override void LoadFromSql()
    {
      DataTable dt = Provider.Select("Region");

      foreach (DataRow row in dt.Rows)
      {
        Region region = new Region(row);
        Add(region);
      }
    }

    public void Add(Region region)
    {
      if (list.Exists(item => item == region))
        return;

      list.Add(region);
    }

    public Region getItem(int id)
    {
      var regions = list.Where(item => item.Id == id);

      return (regions.Count() > 0) ? regions.First() : null;
    }

    public Region getItem(string name)
    {
      var regions = list.Where(item => item.Name == name);

      return (regions.Count() > 0) ? regions.First() : null;
    }
  }
}
