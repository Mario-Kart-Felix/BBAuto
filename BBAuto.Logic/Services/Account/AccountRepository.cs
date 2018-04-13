using System.Collections.Generic;
using System.Threading.Tasks;

namespace BBAuto.Logic.Services.Account
{
  public class AccountRepository : IAccountRepository
  {
    public Task SaveAsync(AccountModel model)
    {
      throw new System.NotImplementedException();
    }

    public Task<AccountModel> GetAsync(int id)
    {
      throw new System.NotImplementedException();
    }

    public Task<IList<AccountModel>> GetAllAsync()
    {
      throw new System.NotImplementedException();
    }
  }
}
