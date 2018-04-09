using System.Data;
using BBAuto.Domain.Common;

namespace BBAuto.Domain.Dictionary
{
  public class Comps : MyDictionary
  {
    private static Comps _uniqueInstance;

    public static Comps GetInstance()
    {
      return _uniqueInstance ?? (_uniqueInstance = new Comps());
    }

    protected override void loadFromSql()
    {
      var dt = provider.Select("Comp");

      FillList(dt);
    }
  }
}
