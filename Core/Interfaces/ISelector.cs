using MyIssue.Core.Entities;

namespace MyIssue.Core.Interfaces
{
    public interface ISelector
    {
        void Send(SettingTextBoxes settings, PersonalDetails details, string description);
    }
}