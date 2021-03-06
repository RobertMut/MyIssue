using Serilog;
using System;

namespace MyIssue.Infrastructure.Files
{
    public class SerilogLogger
    {
        const string template = "App ran into exception\r\n{Exception}";
        const string template2 = "EXCEPTION - {0} - {1}\r\n INNER EXCEPTION {2}\r\n";
        public static ILogger clientLogger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.File(Paths.path + "log.txt")
        .CreateLogger();
        public static ILogger serverLogger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.File("log.txt")
        .CreateLogger();
        public static bool ClientLogException(Exception ex)
        {
            
            clientLogger.Error(ex, template);
            return false;
        }
        public static bool ServerLogException(Exception ex)
        {
            serverLogger.Error(template2, DateTime.Now, ex, ex.InnerException);
            return false;
        }
        public static void Log(string Message)
        {
            serverLogger.Debug(Message);
        }
    }
}
