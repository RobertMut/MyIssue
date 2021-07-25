using System;

namespace MyIssue.Core.Exceptions
{
    public class CommandNotFoundException : Exception
    {
        public CommandNotFoundException() : base()
        {

        }
        public CommandNotFoundException(string message) : base(message)
        {

        }
        public CommandNotFoundException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}