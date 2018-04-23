using System.Collections.Generic;
using BBAuto.Repositories.Entities;
using Insight.Database;

namespace BBAuto.Repositories.Interfaces
{
  [Sql(Schema = "dbo")]
  public interface IDbDealer
  {
    IList<DbDealer> GetDealers();
    DbDealer GetDealerById(int id);
    void DeleteDealer(int id);
    DbDealer UpsertDealer(DbDealer model);
  }
}
