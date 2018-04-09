using System.Data;
using BBAuto.Domain.Common;

namespace BBAuto.Domain.Dictionary
{
  public class Depts : MyDictionary
  {
    private static Depts uniqueInstance;

    public static Depts getInstance()
    {
      if (uniqueInstance == null)
        uniqueInstance = new Depts();

      return uniqueInstance;
    }

    protected override void loadFromSql()
    {
      DataTable dt = provider.Select("Dept");

      FillList(dt);
    }
  }
}
