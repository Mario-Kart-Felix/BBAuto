using System.Data;
using BBAuto.Logic.Common;

namespace BBAuto.Logic.Dictionary
{
  public class ViolationTypes : MyDictionary
  {
    private static ViolationTypes uniqueInstance;

    public static ViolationTypes getInstance()
    {
      if (uniqueInstance == null)
        uniqueInstance = new ViolationTypes();

      return uniqueInstance;
    }

    protected override void loadFromSql()
    {
      DataTable dt = provider.Select("ViolationType");

      FillList(dt);
    }
  }
}
