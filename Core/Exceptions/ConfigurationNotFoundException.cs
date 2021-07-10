using System;

namespace MyIssue.Core.Exceptions
{
    public class ConfigurationNotFoundException : Exception
    {
        public ConfigurationNotFoundException() : base()
        {

        }
        public ConfigurationNotFoundException(string message) : base(message)
        {

        }
        public ConfigurationNotFoundException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
