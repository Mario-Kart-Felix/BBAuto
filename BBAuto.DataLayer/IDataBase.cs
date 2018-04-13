using System.Data;

namespace BBAuto.DataLayer
{
  public interface IDataBase
  {
    DataTable GetRecords(string sql, params object[] Params);
    string GetRecordsOne(string sql, params object[] Params);
  }
}
