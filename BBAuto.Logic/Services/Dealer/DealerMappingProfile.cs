using AutoMapper;
using BBAuto.Repositories.Entities;

namespace BBAuto.Logic.Services.Dealer
{
  public class DealerMappingProfile : Profile
  {
    public DealerMappingProfile()
    {
      CreateMap<DbDealer, DealerModel>().ReverseMap();
    }
  }
}
