using System;
namespace MyIssue.Infrastructure.Files
{
    public static class Paths
    {
        public static string path = Environment.ExpandEnvironmentVariables(@"%APPDATA%\MyIssue\");
        public static string confFile = Environment.ExpandEnvironmentVariables(@"%APPDATA%\MyIssue\configuration.xml");
        public static string userFile = Environment.ExpandEnvironmentVariables(@"%APPDATA%\MyIssue\userData.xml");
    }
}
