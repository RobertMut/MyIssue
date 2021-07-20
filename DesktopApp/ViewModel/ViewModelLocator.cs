using MyIssue.DesktopApp.Model.Exceptions;
using MyIssue.DesktopApp.Model.Services;
using MyIssue.DesktopApp.ViewModel;

namespace MyIssue.DesktopApp.ViewModel
{
    public class ViewModelLocator
    {
        private static IWindowService windowService = new WindowService();
        private static MainWindowViewModel mainWindowViewModel
            = new MainWindowViewModel(windowService);
        private static PromptViewModel promptViewModel
            = new PromptViewModel(windowService);
        public static MainWindowViewModel MainWindowViewModel
        {
            get
            {
                return mainWindowViewModel;
            }
        }
        public static PromptViewModel PromptViewModel
        {
            get
            {
                return promptViewModel;
            }
        }

    }
}
