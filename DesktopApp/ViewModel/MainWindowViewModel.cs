using MyIssue.DesktopApp.Model.Services;
using MyIssue.DesktopApp.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyIssue.DesktopApp.ViewModel
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        public DelegateCommand LoadMainCommand { get; private set; }
        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            LoadCommands();
        }
        private void LoadCommands()
        {
            LoadMainCommand = new DelegateCommand(LoadMain);
        }

        private void LoadMain()
        {
            _regionManager.RequestNavigate("ContentRegion", "Main", Callback);
        }
        private void Callback(NavigationResult res)
        {
            if (!(res.Error is null))
            {
                SerilogLoggerService.LogException(res.Error);
            }
        }
    }
}
