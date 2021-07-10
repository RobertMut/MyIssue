using MyIssue.Core.Interfaces;
using MyIssue.Core.Entities;

namespace MyIssue.DesktopApp.IO
{
    public class ConfigurationBuilder : IConfigurationBuilder
    {
        private ConfigValues cvals;

        private ConfigurationBuilder()
        {
            cvals = new ConfigValues();
        }

        public IConfigurationBuilder SetApplicationPass(string input)
        {
            cvals.ApplicationPass = input;
            return this;
        }
        public IConfigurationBuilder SetCompanyName(string input)
        {
            cvals.CompanyName = input;
            return this;
        }
        public IConfigurationBuilder SetServerAddress(string input)
        {
            cvals.ServerAddress = input;
            return this;
        }
        public IConfigurationBuilder SetPort(string input)
        {
            cvals.Port = input;
            return this;
        }
        public IConfigurationBuilder SetLogin(string input)
        {
            cvals.Login = input;
            return this;
        }
        public IConfigurationBuilder SetPass(string input)
        {
            cvals.Pass = input;
            return this;
        }
        public IConfigurationBuilder SetEmailAddress(string input)
        {
            cvals.EmailAddress = input;
            return this;
        }
        public IConfigurationBuilder SetRecipientAddress(string input)
        {
            cvals.RecipientAddress = input;
            return this;
        }
        public IConfigurationBuilder SetConnectionMethod(bool input)
        {
            cvals.ConnectionMethod = input.ToString();
            return this;
        }
        public IConfigurationBuilder SetSslTsl(bool input)
        {
            cvals.SslTsl = input.ToString();
            return this;
        }
        public ConfigValues Build() => cvals;
        public static ConfigurationBuilder Create() => new ConfigurationBuilder();
    }
}
