using MyIssue.DesktopApp.Misc.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace MyIssue.DesktopApp.ViewModel
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        public DelegateCommand LoadLogoCommand { get; private set; }
        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            LoadCommands();
        }
        private void LoadCommands()
        {
            LoadLogoCommand = new DelegateCommand(LoadLogo);
        }

        private void LoadLogo()
        {
            _regionManager.RequestNavigate("ContentRegion", "Logo", Callback);
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
