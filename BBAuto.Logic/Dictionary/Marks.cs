using System.Data;
using BBAuto.Logic.Common;

namespace BBAuto.Logic.Dictionary
{
  public class Marks : MyDictionary
  {
    private static Marks uniqueInstance;

    public static Marks getInstance()
    {
      if (uniqueInstance == null)
        uniqueInstance = new Marks();

      return uniqueInstance;
    }

    protected override void loadFromSql()
    {
      DataTable dt = provider.Select("Mark");

      FillList(dt);
    }
  }
}
