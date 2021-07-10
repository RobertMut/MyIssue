using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIssue.DesktopApp.IO
{
    public static class Paths
    {
        public static string path = Environment.ExpandEnvironmentVariables(@"%APPDATA%\MyIssue\");
        public static string confFile = Environment.ExpandEnvironmentVariables(@"%APPDATA%\MyIssue\configuration.xml");
        public static string userFile = Environment.ExpandEnvironmentVariables(@"%APPDATA%\MyIssue\userData.xml");
    }
}
