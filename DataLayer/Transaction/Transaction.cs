using System.Data;

namespace DataLayer
{
  public interface ITransaction
  {
    DataTable GetDataTable(string query);
    string GetString(string query);
  }
}
