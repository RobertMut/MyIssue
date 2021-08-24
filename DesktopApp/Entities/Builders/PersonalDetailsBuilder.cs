namespace MyIssue.DesktopApp.Entities.Builders
{
    public class PersonalDetailsBuilder : IDetailsBuilder
    {
        private PersonalDetails det;

        private PersonalDetailsBuilder()
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
        public IDetailsBuilder SetImage(string image)
        {
            det.Image = image;
            return this;
        }
        public PersonalDetails Build() => det;

        public static PersonalDetailsBuilder Create() => new PersonalDetailsBuilder();
    }
}
