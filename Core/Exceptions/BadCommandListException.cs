using System;

namespace MyIssue.Core.Exceptions
{
    public class BadCommandListException : Exception
    {
        public BadCommandListException() : base()
        {

        }
        public BadCommandListException(string message) : base(message)
        {

        }
        public BadCommandListException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}