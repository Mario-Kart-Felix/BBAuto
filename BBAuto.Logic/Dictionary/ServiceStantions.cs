using System.Data;
using BBAuto.Logic.Common;

namespace BBAuto.Logic.Dictionary
{
  public class ServiceStantions : MyDictionary
  {
    private static ServiceStantions uniqueInstance;

    public static ServiceStantions getInstance()
    {
      if (uniqueInstance == null)
        uniqueInstance = new ServiceStantions();

      return uniqueInstance;
    }

    protected override void loadFromSql()
    {
      DataTable dt = provider.Select("ServiceStantion");

      FillList(dt);
    }
  }
}
