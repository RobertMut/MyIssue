using System;

namespace MyIssue.DesktopApp.Model.Exceptions
{
    public interface IDesktopExceptionHandler
    {
        void HandleExceptions(Exception e);
    }
}