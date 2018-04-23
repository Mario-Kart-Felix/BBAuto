namespace BBAuto.Repositories
{
  public interface IRepository<T> where T : class
  {
    T DbRepository { get; }
  }
}
