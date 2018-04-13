using System.Data;
using BBAuto.Logic.Common;

namespace BBAuto.Logic.Dictionary
{
  public class Owners : MyDictionary
  {
    private static Owners uniqueInstance;

    public static Owners getInstance()
    {
      if (uniqueInstance == null)
        uniqueInstance = new Owners();

      return uniqueInstance;
    }

    protected override void loadFromSql()
    {
      DataTable dt = provider.Select("Owner");

      FillList(dt);
    }
  }
}
