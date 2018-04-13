using System.Collections.Generic;
using System.Data;
using System.Linq;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Entities;
using BBAuto.Logic.ForCar;

namespace BBAuto.Logic.Lists
{
  public class STSList : MainList
  {
    private List<STS> list;
    private static STSList uniqueInstance;

    private STSList()
    {
      list = new List<STS>();

      LoadFromSql();
    }

    public static STSList getInstance()
    {
      if (uniqueInstance == null)
        uniqueInstance = new STSList();

      return uniqueInstance;
    }

    protected override void LoadFromSql()
    {
      DataTable dt = Provider.Select("STS");

      foreach (DataRow row in dt.Rows)
      {
        STS sts = new STS(row);
        Add(sts);
      }
    }

    public void Add(STS sts)
    {
      if (list.Exists(x => x == sts))
        return;

      list.Add(sts);
    }

    public void Delete(Car car)
    {
      STS sts = getItem(car);

      list.Remove(sts);

      sts.Delete();
    }

    public STS getItem(Car car)
    {
      var STSs = list.Where(s => s.Car.Id == car.Id);

      return (STSs.Count() > 0) ? STSs.First() : car.createSTS();
    }
  }
}
