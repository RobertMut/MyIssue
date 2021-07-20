using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace MyIssue.DesktopApp.Model.Exceptions
{
    public class DesktopExceptionHandler : IDesktopExceptionHandler
    {
        public void HandleExceptions(Exception e)
        {
            MessageBox.Show(
                string.Format("Exception occurred.\r\n{0}\r\n{1}", e.Message, e.StackTrace), "Critical error!",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
