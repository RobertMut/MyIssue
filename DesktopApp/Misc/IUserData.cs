using MyIssue.DesktopApp.Model;

namespace MyIssue.DesktopApp.Misc
{
    public interface IUserData
    {
        PersonalDetails Load(string image, string company);
    }
}