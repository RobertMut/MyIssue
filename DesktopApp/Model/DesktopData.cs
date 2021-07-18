using System;
using System.IO;
using System.Xml.Linq;
using MyIssue.Core.Entities.Builders;
using MyIssue.Infrastructure.Files;
using MyIssue.Core.Exceptions;

namespace MyIssue.DesktopApp.Model
{
    public class DesktopData : IDesktopData
    {
        private XDocument file;
        private readonly string applicationPass;
        public DesktopData()
        {
            if (!File.Exists(Paths.confFile)) throw new ConfigurationNotFoundException();
            file = OpenConfiguration.OpenConfig(Paths.confFile);
            applicationPass = DecryptedValue.GetValue(file, "applicationPass");
        }
        public void Load()
        {
            if (Convert.ToBoolean(ConfigValue.GetValue("isSmtp", file)))
                DesktopConfig.Config =
                    ConfigValuesBuilder
                    .Create()
                        .SetApplicationPass(applicationPass)
                        .SetCompanyName(ConfigValue.GetValue("companyName", file))
                        .SetServerAddress(DecryptedValue.GetValue(file, "serverAddress", applicationPass))
                        .SetPort(DecryptedValue.GetValue(file, "port", applicationPass))
                        .SetLogin(DecryptedValue.GetValue(file, "login", applicationPass))
                        .SetPass(DecryptedValue.GetValue(file, "pass", applicationPass))
                        .SetSslTsl(Convert.ToBoolean(
                            ConfigValue.GetValue("sslTsl", file)))
                        .SetEmailAddress(DecryptedValue.GetValue(file, "emailAddress", applicationPass))
                        .SetRecipientAddress(DecryptedValue.GetValue(file, "recipientAddress", applicationPass))
                        .SetConnectionMethod(Convert.ToBoolean(
                            ConfigValue.GetValue("isSmtp", file)))
                        .SetImage(ConfigValue.GetValue("image", file))
                    .Build();
            else
                DesktopConfig.Config =
                    ConfigValuesBuilder
                    .Create()
                        .SetApplicationPass(applicationPass)
                        .SetCompanyName(ConfigValue.GetValue("companyName", file))
                        .SetServerAddress(DecryptedValue.GetValue(file, "serverAddress", applicationPass))
                        .SetPort(DecryptedValue.GetValue(file, "port", applicationPass))
                        .SetLogin(DecryptedValue.GetValue(file, "login", applicationPass))
                        .SetPass(DecryptedValue.GetValue(file, "pass", applicationPass))
                        .SetConnectionMethod(Convert.ToBoolean(
                            ConfigValue.GetValue("isSmtp", file)))
                        .SetImage(ConfigValue.GetValue("image", file))
                    .Build();
        }

    }
}
