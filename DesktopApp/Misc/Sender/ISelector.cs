using MyIssue.DesktopApp.Model;

namespace MyIssue.DesktopApp.Misc.Sender
{
    public interface ISelector
    {
        void Send(SettingTextBoxes settings, PersonalDetails details, string description);
    }
}