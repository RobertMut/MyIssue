using MyIssue.Core.Interfaces;
using System;
using System.Windows;

namespace MyIssue.Core.Exceptions
{
    public class ExceptionMessageBox : IExceptionMessageBox
    {
        public void ShowException(Exception e)
        {
            MessageBox.Show("Exception occurred.\r\n" + e.Message, "Critical error!",
            MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
