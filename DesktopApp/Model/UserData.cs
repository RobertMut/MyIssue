using MyIssue.Core.Entities.Builders;
using MyIssue.Core.Exceptions;
using MyIssue.Core.Interfaces;
using MyIssue.Infrastructure.Files;

namespace MyIssue.DesktopApp.Model
{
    public class UserData : IUserData
    {
        private IExceptionMessageBox _exceptionMessage;
        public void Load()
        {
            try
            {
                var userFile = OpenConfiguration.OpenConfig(Paths.userFile);
                UserDetails.details = PersonalDetailsBuilder
                   .Create()
                       .SetName(ConfigValue.GetValue("name", userFile))
                       .SetSurname(ConfigValue.GetValue("surname", userFile))
                       .SetCompany(DesktopConfig.Config.CompanyName)
                       .SetPhone(ConfigValue.GetValue("phone", userFile))
                       .SetEmail(ConfigValue.GetValue("email", userFile))
                   .Build();
            }
            catch (ConfigurationNotFoundException e)
            {
                _exceptionMessage.ShowException(e);
            }

        }
    }
}
