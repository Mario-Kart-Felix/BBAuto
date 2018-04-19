using System.Collections.Generic;
using System.Threading.Tasks;
using BBAuto.Repositories.Entities;
using Insight.Database;

namespace BBAuto.Repositories.Interfaces
{
  [Sql(Schema = "dbo")]
  public interface IDbDealer
  {
    Task<IList<DbDealer>> GetDealersAsync();
  }
}
