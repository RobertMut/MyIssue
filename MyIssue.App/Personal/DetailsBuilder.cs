using MyIssue.Core.Entities;
using MyIssue.Core.Interfaces;

namespace MyIssue.DesktopApp.Personal
{
    public class DetailsBuilder : IDetailsBuilder
    {
        private PersonalDetails det;

        private DetailsBuilder()
        {
            det = new PersonalDetails();
        }
        public IDetailsBuilder SetName(string name)
        {
            det.Name = name;
            return this;
        }
        public IDetailsBuilder SetSurname(string surname)
        {
            det.Surname = surname;
            return this;
        }
        public IDetailsBuilder SetCompany(string company)
        {
            det.Company = company;
            return this;
        }
        public IDetailsBuilder SetPhone(string phone)
        {
            det.Phone = phone;
            return this;
        }
        public IDetailsBuilder SetEmail(string email)
        {
            det.Email = email;
            return this;
        }
        public PersonalDetails Build() => det;

        public static DetailsBuilder Create() => new DetailsBuilder();
    }
}
