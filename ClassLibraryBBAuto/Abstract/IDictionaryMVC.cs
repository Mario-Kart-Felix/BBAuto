namespace BBAuto.Domain.Abstract
{
  public interface IDictionaryMVC
  {
    string Name { get; set; }
    string Text { get; set; }
    void Save();
  }
}
