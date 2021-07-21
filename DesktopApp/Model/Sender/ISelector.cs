using MyIssue.Core.Entities;

namespace MyIssue.DesktopApp.Model.Sender
{
    interface ISelector
    {
        void Send(SettingTextBoxes settings, PersonalDetails details, string description);
    }
}