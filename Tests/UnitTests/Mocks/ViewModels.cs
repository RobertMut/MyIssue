using Moq;
using MyIssue.DesktopApp.ViewModel;
using MyIssue.DesktopApp.Views;
using Prism.Regions;

namespace MyIssue.UnitTests.Mocks
{
    public class ViewModels
    {
        public Mock<IRegionManager> RegionManagerMock { get; private set; }
        public MainWindowViewModel MainWindowViewModel { get; private set; }
        public PromptViewModel PromptViewModel { get; private set; }
        public SettingsViewModel SettingsViewModel { get; private set; }
        public MainViewModel MainViewModel { get; private set; }
        public ViewModels()
        {
            RegionManagerMock = new Mock<IRegionManager>();
            RegionManagerMock.Setup(x => x.RegisterViewWithRegion("ContentRegion", typeof(DesktopApp.Views.Main)));
            RegionManagerMock.Setup(x => x.RegisterViewWithRegion("ContentRegion", typeof(Prompt)));
            RegionManagerMock.Setup(x => x.RegisterViewWithRegion("ContentRegion", typeof(Settings)));
            MainWindowViewModel = new MainWindowViewModel(RegionManagerMock.Object);
            PromptViewModel = new PromptViewModel(RegionManagerMock.Object);
            MainViewModel = new MainViewModel(RegionManagerMock.Object);
            SettingsViewModel = new SettingsViewModel(RegionManagerMock.Object);
            MainViewModel.Settings.ApplicationPass = "1234";
        }
    }
}
