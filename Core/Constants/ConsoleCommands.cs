namespace MyIssue.Core.Constants
{
    public class ConsoleCommands
    {
        public const string login = "Login\r\n<EOF>\r\n";
        public const string loginParameters = "{0}\r\n<NEXT>\r\n{1}\r\n<EOF>\r\n";
        public const string newTask = "CreateTask\r\n<EOF>\r\n";
        public const string newTaskParameters = "{0}\r\n<NEXT>\r\n{1}\r\n<NEXT>\r\n{2}\r\n<NEXT>\r\n{3}\r\n<NEXT>\r\n{4}\r\n<EOF>\r\n";
        public const string logout = "Logout\r\n<EOF>\r\n";
    }
}
