using System;

public class NotSufficientPermissionsException : Exception
{
    public NotSufficientPermissionsException() : base()
    {

    }
    public NotSufficientPermissionsException(string message) : base(message)
    {

    }
    public NotSufficientPermissionsException(string message, Exception inner) : base(message, inner)
    {

    }
}