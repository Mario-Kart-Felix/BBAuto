using BBAuto.Logic;
using Castle.Windsor;

namespace BBAuto.App.config
{
  public static class WindsorConfiguration
  {
    public static WindsorContainer Container { get; private set; }

    public static void Register()
    {
      Container = new WindsorContainer();

      Container.Install(new WindsorFormInstaller());
      Container.Install(new WindsorInstaller());
    }
  }
}
