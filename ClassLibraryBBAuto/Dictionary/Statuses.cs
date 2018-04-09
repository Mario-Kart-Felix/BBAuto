using System.Data;
using BBAuto.Logic.Common;

namespace BBAuto.Logic.Dictionary
{
  public class Statuses : MyDictionary
  {
    private static Statuses uniqueInstance;

    public static Statuses getInstance()
    {
      if (uniqueInstance == null)
        uniqueInstance = new Statuses();

      return uniqueInstance;
    }

    protected override void loadFromSql()
    {
      DataTable dt = provider.Select("Status");

      FillList(dt);
    }
  }
}
