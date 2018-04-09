namespace BBAuto.Logic.Abstract
{
  public interface IDictionaryMvc
  {
    string Name { get; set; }
    string Text { get; set; }
    void Save();
  }
}
