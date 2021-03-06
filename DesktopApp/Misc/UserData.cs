using MyIssue.Core.Exceptions;
using MyIssue.Core.Interfaces;
using MyIssue.Infrastructure.Files;
using System;
using System.Windows;
using MyIssue.DesktopApp.Model;
using MyIssue.DesktopApp.Model.Builders;

namespace MyIssue.DesktopApp.Misc
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
                       .SetName(ConfigValue.GetValue<string>("name", userFile))
                       .SetSurname(ConfigValue.GetValue<string>("surname", userFile))
                       .SetCompany(company)
                       .SetPhone(ConfigValue.GetValue<string>("phone", userFile))
                       .SetEmail(ConfigValue.GetValue<string>("email", userFile))
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
            } catch (NullReferenceException e)
            {
                SerilogLogger.ClientLogException(e);
                return (PersonalDetails)DependencyProperty.UnsetValue;
            }
        }
    }
}
