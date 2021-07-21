using MyIssue.Core.Entities;
using MyIssue.Core.Entities.Builders;
using MyIssue.Core.Exceptions;
using MyIssue.Core.Interfaces;
using MyIssue.Infrastructure.Files;

namespace MyIssue.DesktopApp.Model
{
    public class UserData : IUserData
    {
        //private IExceptionMessageBox _exceptionMessage;
        public PersonalDetails Load(string image, string company)
        {
            try
            {
                var userFile = OpenConfiguration.OpenConfig(Paths.userFile);
                return PersonalDetailsBuilder
                   .Create()
                       .SetName(ConfigValue.GetValue("name", userFile))
                       .SetSurname(ConfigValue.GetValue("surname", userFile))
                       .SetCompany(company)
                       .SetPhone(ConfigValue.GetValue("phone", userFile))
                       .SetEmail(ConfigValue.GetValue("email", userFile))
                       .SetImage(image)
                   .Build();
            }
            catch (ConfigurationNotFoundException)
            {
                return PersonalDetailsBuilder
                    .Create()
                        .SetCompany(company)
                        .SetImage(image)
                    .Build();
            }
        }
    }
}
