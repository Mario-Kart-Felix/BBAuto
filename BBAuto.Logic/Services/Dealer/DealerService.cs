using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BBAuto.Repositories;
using Common;

namespace BBAuto.Logic.Services.Dealer
{
  public class DealerService : IDealerService
  {
    private readonly IDbContext _dbContext;

    public DealerService(IDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public async Task<IList<DealerModel>> GetDealersAsync()
    {
      var dbDealers = await _dbContext.Dealer.GetDealersAsync();

      //return ObjectMapper.Map<IList<DealerModel>>(dbDealers);
      return Mapper.Map<IList<DealerModel>>(dbDealers);
    }
  }
}
