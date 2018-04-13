using System.Data;
using BBAuto.Logic.Common;

namespace BBAuto.Logic.Dictionary
{
  public class Colors : MyDictionary
  {
    private static Colors _uniqueInstance;

    public static Colors GetInstance()
    {
      if (_uniqueInstance == null)
        _uniqueInstance = new Colors();

      return _uniqueInstance;
    }

    protected override void loadFromSql()
    {
      DataTable dt = provider.Select("Color");

      FillList(dt);
    }
  }
}
