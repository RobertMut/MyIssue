using System;
using System.Runtime.Serialization;

namespace MyIssue.Core.Exceptions
{
    [Serializable]
    public class InvaidOperationException : Exception
    {
        public InvaidOperationException()
        {
        }

        public InvaidOperationException(string message) : base(message)
        {
        }

        public InvaidOperationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvaidOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}