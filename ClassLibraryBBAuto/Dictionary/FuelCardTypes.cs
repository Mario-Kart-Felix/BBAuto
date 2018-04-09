using System.Data;
using BBAuto.Domain.Common;

namespace BBAuto.Domain.Dictionary
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
