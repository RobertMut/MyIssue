using Moq;
using MyIssue.DesktopApp.ViewModel;
using MyIssue.DesktopApp.Views;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Mocks
{
    public class ViewModels
    {
        public Mock<IRegionManager> RegionManagerMock { get; private set; }
        public MainWindowViewModel MainWindowViewModel { get; private set; }
        public PromptViewModel PromptViewModel { get; private set; }
        public SettingsViewViewModel SettingsViewModel { get; private set; }
        public MainViewModel MainViewModel { get; private set; }
        public ViewModels()
        {
            RegionManagerMock = new Mock<IRegionManager>();
            RegionManagerMock.Setup(x => x.RegisterViewWithRegion("ContentRegion", typeof(Main)));
            RegionManagerMock.Setup(x => x.RegisterViewWithRegion("ContentRegion", typeof(Prompt)));
            RegionManagerMock.Setup(x => x.RegisterViewWithRegion("ContentRegion", typeof(SettingsView)));
            MainWindowViewModel = new MainWindowViewModel(RegionManagerMock.Object);
            PromptViewModel = new PromptViewModel(RegionManagerMock.Object);
            MainViewModel = new MainViewModel(RegionManagerMock.Object);
            SettingsViewModel = new SettingsViewViewModel(RegionManagerMock.Object);
            MainViewModel.Settings.ApplicationPass = "1234";
        }
    }
}
