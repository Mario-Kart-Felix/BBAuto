using System.Collections.Generic;
using System.Threading.Tasks;

namespace BBAuto.Logic.Services.Account
{
  public interface IAccountRepository
  {
    Task SaveAsync(AccountModel model);

    Task<AccountModel> GetAsync(int id);

    Task<IList<AccountModel>> GetAllAsync();
  }
}
