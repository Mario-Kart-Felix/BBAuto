using Serilog;

namespace BBAuto.Logic.Logger
{
  public static class LogManager
  {
    public static ILogger Logger => new LoggerConfiguration()
      .MinimumLevel.Debug()
      .WriteTo.ColoredConsole()
      .WriteTo.RollingFile(@"\\bbmru08\Programs\Utility\BBAuto\Log\{Date}.txt")
      .CreateLogger();
  }
}
