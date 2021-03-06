using MyIssue.DesktopApp.Views;
using MyIssue.DesktopApp.ViewModel;
using MyIssue.Infrastructure.Files;
using Prism.Ioc;
using Prism.Regions;
using Prism.Unity;
using System.IO;
using System.Windows;
using System.Windows.Threading;
using Prism.Mvvm;

namespace MyIssue.DesktopApp
{
    /// <summary>
    /// Interaction logic for App.xaml 
    /// </summary>
    public partial class App : PrismApplication
    {
        public App()
        {
            if (!Directory.Exists(Paths.path)) Directory.CreateDirectory(Paths.path);
            ViewModelLocationProvider.Register<MainWindow, MainWindowViewModel>();
            ViewModelLocationProvider.Register<Logo, LogoViewModel>();
            ViewModelLocationProvider.Register<Main, MainViewModel>();
            ViewModelLocationProvider.Register<Prompt, PromptViewModel>();
            ViewModelLocationProvider.Register<Settings, SettingsViewModel>();
        }
        protected override void OnInitialized()
        {
            base.OnInitialized();
            var regionManag = Container.Resolve<IRegionManager>();
            regionManag.RegisterViewWithRegion("ContentRegion", typeof(Logo));
            regionManag.RegisterViewWithRegion("ContentRegion", typeof(Main));
            regionManag.RegisterViewWithRegion("ContentRegion", typeof(Prompt));
            regionManag.RegisterViewWithRegion("ContentRegion", typeof(Settings));

        }
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Logo>("Logo");
            containerRegistry.RegisterForNavigation<Main>("Main");
            containerRegistry.RegisterForNavigation<Prompt>("Prompt");
            containerRegistry.RegisterForNavigation<Settings>("SettingsView");
        }
        void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            SerilogLogger.ClientLogException(e.Exception);
            e.Handled = true;
        }
        protected override void InitializeShell(Window shell)
        {
            base.InitializeShell(shell);
            Application.Current.MainWindow.Show();
        }
    }
}
