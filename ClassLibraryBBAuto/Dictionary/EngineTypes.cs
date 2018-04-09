using System.Data;
using BBAuto.Logic.Common;

namespace BBAuto.Logic.Dictionary
{
  public class EngineTypes : MyDictionary
  {
    private static EngineTypes uniqueInstance;

    public static EngineTypes getInstance()
    {
      if (uniqueInstance == null)
        uniqueInstance = new EngineTypes();

      return uniqueInstance;
    }

    protected override void loadFromSql()
    {
      DataTable dt = provider.Select("EngineType");

      FillList(dt);
    }
  }
}
