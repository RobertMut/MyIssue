namespace MyIssue.DesktopApp.Model.Builders
{
    public interface IDetailsBuilder
    {
        IDetailsBuilder SetName(string name);
        IDetailsBuilder SetSurname(string surname);
        IDetailsBuilder SetCompany(string company);
        IDetailsBuilder SetPhone(string phone);
        IDetailsBuilder SetEmail(string email);
        IDetailsBuilder SetImage(string image);
        PersonalDetails Build();
    }
}
