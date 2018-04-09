using System.Data;

namespace BBAuto.DataLayer.Transaction
{
  public interface ITransaction
  {
    DataTable GetDataTable(string query);
    string GetString(string query);
  }
}
