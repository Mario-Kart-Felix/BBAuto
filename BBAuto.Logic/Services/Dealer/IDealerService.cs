using System.Collections.Generic;
using System.Threading.Tasks;

namespace BBAuto.Logic.Services.Dealer
{
  public interface IDealerService
  {
    Task<IList<DealerModel>> GetDealersAsync();
  }
}
