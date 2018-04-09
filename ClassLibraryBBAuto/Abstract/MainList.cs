using BBAuto.Logic.DataBase;

namespace BBAuto.Logic.Abstract
{
  public abstract class MainList
  {
    protected IProvider Provider;

    protected abstract void LoadFromSql();

    protected MainList()
    {
      Provider = DataBase.Provider.GetProvider();
    }

    public void ReLoad()
    {
      LoadFromSql();
    }
  }
}
