using System.Data;
using BBAuto.Domain.Common;

namespace BBAuto.Domain.Dictionary
{
  public class Positions : MyDictionary
  {
    private static Positions uniqueInstance;

    public static Positions getInstance()
    {
      if (uniqueInstance == null)
        uniqueInstance = new Positions();

      return uniqueInstance;
    }

    protected override void loadFromSql()
    {
      DataTable dt = provider.Select("Position");

      FillList(dt);
    }
  }
}
