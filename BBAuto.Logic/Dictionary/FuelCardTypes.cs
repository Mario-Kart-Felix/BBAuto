using System.Data;
using BBAuto.Logic.Common;

namespace BBAuto.Logic.Dictionary
{
  public class FuelCardTypes : MyDictionary
  {
    private static FuelCardTypes uniqueInstance;

    public static FuelCardTypes getInstance()
    {
      if (uniqueInstance == null)
        uniqueInstance = new FuelCardTypes();

      return uniqueInstance;
    }

    protected override void loadFromSql()
    {
      DataTable dt = provider.Select("FuelCardType");

      FillList(dt);
    }
  }
}
