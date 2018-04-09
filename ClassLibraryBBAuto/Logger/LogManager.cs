using Serilog;

namespace BBAuto.Logic.Logger
{
  public static class LogManager
  {
    private static ILogger logger = new LoggerConfiguration()
      .MinimumLevel.Debug()
      .WriteTo.ColoredConsole()
      .WriteTo.RollingFile(@"\\bbmru08\Programs\Utility\BBAuto\Log\{Date}.txt")
      .CreateLogger();

    public static ILogger Logger
    {
      get { return logger; }
    }
  }
}
