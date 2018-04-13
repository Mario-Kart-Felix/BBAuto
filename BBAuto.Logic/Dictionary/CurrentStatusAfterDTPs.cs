using System.Data;
using BBAuto.Logic.Common;

namespace BBAuto.Logic.Dictionary
{
  public class CurrentStatusAfterDTPs : MyDictionary
  {
    private static CurrentStatusAfterDTPs uniqueInstance;

    public static CurrentStatusAfterDTPs getInstance()
    {
      if (uniqueInstance == null)
        uniqueInstance = new CurrentStatusAfterDTPs();

      return uniqueInstance;
    }

    protected override void loadFromSql()
    {
      DataTable dt = provider.Select("CurrentStatusAfterDTP");

      FillList(dt);
    }
  }
}
