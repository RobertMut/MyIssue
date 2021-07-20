using MyIssue.DesktopApp.Model.Exceptions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace MyIssue.DesktopApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IDesktopExceptionHandler exceptionHandler;
        public App()
        {
            exceptionHandler = new DesktopExceptionHandler();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            AppDomain.CurrentDomain.UnhandledException +=
                new UnhandledExceptionEventHandler(DomainUnhandledExceptionHandler);
            Current.DispatcherUnhandledException +=
                new DispatcherUnhandledExceptionEventHandler(App_DispatcherUnhandledException);
            base.OnStartup(e);
        }

        private void DomainUnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            exceptionHandler.HandleExceptions(new Exception("Domain Unhadled exception")); //TODO
        }

        void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            exceptionHandler.HandleExceptions(e.Exception);
        }
    }
}
