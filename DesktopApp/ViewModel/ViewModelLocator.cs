using MyIssue.DesktopApp.Model.Services;
using MyIssue.DesktopApp.ViewModel.Converters;
using System.Windows.Data;

namespace MyIssue.DesktopApp.ViewModel
{
    public class ViewModelLocator
    {
        private static IWindowService windowService = new WindowService();
        private static IValueConverter localImageConverter = new ImageConverter();
        private static IValueConverter localBooleanStringConverter = new BooleanStringConverter();
        private static MainWindowViewModel mainWindowViewModel
            = new MainWindowViewModel(windowService);
        private static PromptViewModel promptViewModel
            = new PromptViewModel(windowService);
        private static SettingsWindowViewModel settingsWindowViewModel
            = new SettingsWindowViewModel(windowService);
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
        public static SettingsWindowViewModel SettingsWindowViewModel
        {
            get
            {
                return settingsWindowViewModel;
            }
        }
        public static IValueConverter LocalImageConverter
        {
            get
            {
                return localImageConverter;
            }
        }
        public static IValueConverter LocalBooleanStringConverter
        {
            get
            {
                return localBooleanStringConverter;
            }
        }

    }
}
