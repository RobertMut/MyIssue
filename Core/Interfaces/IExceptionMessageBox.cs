using System;

namespace MyIssue.Core.Interfaces
{
    public interface IExceptionMessageBox
    {
        void ShowException(Exception e);
    }
}