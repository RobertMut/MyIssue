using MyIssue.Core.Entities;

namespace MyIssue.Core.Interfaces
{
    public interface IDetailsBuilder
    {
        IDetailsBuilder SetName(string name);
        IDetailsBuilder SetSurname(string surname);
        IDetailsBuilder SetCompany(string company);
        IDetailsBuilder SetPhone(string phone);
        IDetailsBuilder SetEmail(string email);
        PersonalDetails Build();
    }
}
