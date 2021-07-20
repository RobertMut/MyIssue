namespace MyIssue.DesktopApp.Model.Services
{
    public interface IWindowService
    {
        void ClosePrompt();
        void CloseSettings();
        void ShowPrompt();
        void ShowSettings();
        void ShowMainWindow();
        void CloseMainWindow();
    }
}