using BBAuto.Logic.Common;
using BBAuto.Logic.DataBase;

namespace BBAuto.Logic.Abstract
{
  public abstract class MainDictionary
  {
    protected string _fileBegin;
    protected static IProvider _provider;

    internal abstract object[] getRow();

    public virtual void Save()
    {
    }

    internal virtual void Delete()
    {
    }

    protected MainDictionary()
    {
      _provider = Provider.GetProvider();
    }

    protected void DeleteFile(string newFile)
    {
      if ((_fileBegin != string.Empty) && (_fileBegin != newFile))
        WorkWithFiles.Delete(_fileBegin);
    }

    public int ID { get; protected set; }
  }
}
