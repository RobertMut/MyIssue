using MyIssue.Core.Entities;

namespace MyIssue.Core.Interfaces
{
    public interface IUserData
    {
        PersonalDetails Load(string image, string company);
    }
}