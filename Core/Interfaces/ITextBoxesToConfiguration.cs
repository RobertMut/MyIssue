using MyIssue.Core.Entities;

namespace MyIssue.Core.Interfaces
{
    public interface ITextBoxesToConfiguration
    {
        void DoWriting(bool isSmtp, SettingTextBoxes textBoxes);
    }
}