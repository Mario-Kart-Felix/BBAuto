using System.Collections.Generic;
using AutoMapper;
using BBAuto.Repositories;
using BBAuto.Repositories.Entities;

namespace BBAuto.Logic.Services.Dealer
{
  public class DealerService : IDealerService
  {
    private readonly IDbContext _dbContext;

    public DealerService(IDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public DealerModel Save(DealerModel model)
    {
      var dbModel = Mapper.Map<DbDealer>(model);

      var result = _dbContext.Dealer.UpsertDealer(dbModel);

      return Mapper.Map<DealerModel>(result);
    }

    public IList<DealerModel> GetDealers()
    {
      var dbDealers = _dbContext.Dealer.GetDealers();

      return Mapper.Map<IList<DealerModel>>(dbDealers);
    }

    public DealerModel GetDealer(int id)
    {
      return Mapper.Map<DealerModel>(_dbContext.Dealer.GetDealerById(id));
    }

    public void Delete(int id)
    {
      _dbContext.Dealer.DeleteDealer(id);
    }
  }
}
