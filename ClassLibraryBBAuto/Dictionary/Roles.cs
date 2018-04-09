using System.Data;
using BBAuto.Logic.Common;

namespace BBAuto.Logic.Dictionary
{
  public class Roles : MyDictionary
  {
    private static Roles uniqueInstance;

    public static Roles getInstance()
    {
      if (uniqueInstance == null)
        uniqueInstance = new Roles();

      return uniqueInstance;
    }

    protected override void loadFromSql()
    {
      DataTable dt = provider.Select("Role");

      FillList(dt);
    }
  }
}
