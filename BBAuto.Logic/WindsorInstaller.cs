using System.Configuration;
using BBAuto.Logic.Services.Dealer;
using BBAuto.Repositories;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Common;

namespace BBAuto.Logic
{
  public class WindsorInstaller : IWindsorInstaller
  {
    public void Install(IWindsorContainer container, IConfigurationStore store)
    {
      container.Register(Component.For(typeof(IRepository<>))
        .ImplementedBy(typeof(Repository<>))
        .DependsOn(new
        {
          connectionStringSettings =
          ConfigurationManager.ConnectionStrings[Consts.Config.ConnectionName]
        })
        .LifeStyle.Transient);

      container.Register(Component.For<IDbContext>()
        .ImplementedBy<DbContext>()
        .DependsOn(new
        {
          ConnectionStringSettings = ConfigurationManager.ConnectionStrings[Consts.Config.ConnectionName]
        })
        .LifestyleTransient());

      container.Register(Component.For<IDealerService>()
        .ImplementedBy<DealerService>()
        .LifestyleTransient());
    }
  }
}
