using System.Data;
using BBAuto.Logic.Common;

namespace BBAuto.Logic.Dictionary
{
  public class RepairTypes : MyDictionary
  {
    private static RepairTypes uniqueInstance;

    public static RepairTypes getInstance()
    {
      if (uniqueInstance == null)
        uniqueInstance = new RepairTypes();

      return uniqueInstance;
    }

    protected override void loadFromSql()
    {
      DataTable dt = provider.Select("RepairType");

      FillList(dt);
    }
  }
}
