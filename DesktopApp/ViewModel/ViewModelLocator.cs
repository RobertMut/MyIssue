using MyIssue.DesktopApp.Model.Exceptions;
using MyIssue.DesktopApp.Model.Services;
using MyIssue.DesktopApp.ViewModel;

namespace MyIssue.DesktopApp.ViewModel
{
    public class ViewModelLocator
    {
        private static readonly IWindowService windowService = new WindowService();
        private static readonly MainWindowViewModel mainWindowViewModel
            = new MainWindowViewModel(windowService);

        public static MainWindowViewModel MainWindowViewModel
        {
            get
            {
                return mainWindowViewModel;
            }
        }

    }
}
