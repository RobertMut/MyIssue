using System.IO;
using System.Xml.Linq;
using MyIssue.Core.Entities.Builders;
using MyIssue.Infrastructure.Files;
using MyIssue.Core.Entities;
using MyIssue.Core.Exceptions;
using MyIssue.Core.Interfaces;
using MyIssue.DesktopApp.Misc.Utility;

namespace MyIssue.DesktopApp.Misc
{
    public class DesktopData : IDesktopData
    {
        public SettingTextBoxes Load()
        {
            if (!File.Exists(Paths.confFile)) throw new ConfigurationNotFoundException("File does not exist");
            XDocument file = OpenConfiguration.OpenConfig(Paths.confFile);
            string applicationPass = DecryptedValue.GetValue(file, "applicationPass");
            if (ConfigValue.GetValue<bool>("isSmtp", file))
                return
                    ConfigValuesBuilder
                    .Create()
                        .SetApplicationPass(applicationPass)
                        .SetCompanyName(ConfigValue.GetValue<string>("companyName", file))
                        .SetServerAddress(DecryptedValue.GetValue(file, "serverAddress", applicationPass))
                        .SetPort(DecryptedValue.GetValue(file, "port", applicationPass))
                        .SetLogin(DecryptedValue.GetValue(file, "login", applicationPass))
                        .SetPass(DecryptedValue.GetValue(file, "pass", applicationPass))
                        .SetSslTsl(ConfigValue.GetValue<bool>("sslTsl", file))
                        .SetEmailAddress(DecryptedValue.GetValue(file, "emailAddress", applicationPass))
                        .SetRecipientAddress(DecryptedValue.GetValue(file, "recipientAddress", applicationPass))
                        .SetConnectionMethod(ConfigValue.GetValue<bool>("isSmtp", file))
                        .SetImage(ConfigValue.GetValue<string>("image", file))
                    .Build();
            else
                return
                    ConfigValuesBuilder
                    .Create()
                        .SetApplicationPass(applicationPass)
                        .SetCompanyName(ConfigValue.GetValue<string>("companyName", file))
                        .SetServerAddress(DecryptedValue.GetValue(file, "serverAddress", applicationPass))
                        .SetPort(DecryptedValue.GetValue(file, "port", applicationPass))
                        .SetLogin(DecryptedValue.GetValue(file, "login", applicationPass))
                        .SetPass(DecryptedValue.GetValue(file, "pass", applicationPass))
                        .SetConnectionMethod(ConfigValue.GetValue<bool>("isSmtp", file))
                        .SetImage(ConfigValue.GetValue<string>("image", file))
                    .Build();
        }

    }
}
