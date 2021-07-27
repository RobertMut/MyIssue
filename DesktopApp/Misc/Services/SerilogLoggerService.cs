using MyIssue.Infrastructure.Files;
using Serilog;
using System;

namespace MyIssue.DesktopApp.Misc.Services
{
    public class SerilogLoggerService //TODO: MOVE TO ANOTHER PROJECT
    {
        const string template = "App ran into exception\r\n{Exception}";
        public static ILogger logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.File(Paths.path + "log.txt")
        .CreateLogger();
        public static bool LogException(Exception ex)
        {
            logger.Error(ex, template);
            return false;
        }
    }
}
