using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace BBAuto.Domain.Logger
{
    public static class LogManager
    {
        private static ILogger logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.ColoredConsole()
            .WriteTo.RollingFile(@"\\bbmru08\Programs\Utility\BBAuto\Log\{Date}.txt")
            .CreateLogger();

        public static ILogger Logger { get { return logger; } }
    }
}
