using System.Collections.Generic;

namespace BBAuto.Logic.Services.Dealer
{
  public interface IDealerService
  {
    DealerModel Save(DealerModel model);

    IList<DealerModel> GetDealers();

    DealerModel GetDealer(int id);

    void Delete(int id);
  }
}
