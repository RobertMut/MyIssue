namespace MyIssue.DesktopApp.Entities.Builders
{
    public class ConfigValuesBuilder : IConfigurationBuilder
    {
        private SettingTextBoxes cvals;

        private ConfigValuesBuilder()
        {
            cvals = new SettingTextBoxes();
        }

        public IConfigurationBuilder SetApplicationPass(string applicationPass)
        {
            cvals.ApplicationPass = applicationPass;
            return this;
        }
        public IConfigurationBuilder SetCompanyName(string companyName)
        {
            cvals.CompanyName = companyName;
            return this;
        }
        public IConfigurationBuilder SetServerAddress(string serverAddress)
        {
            cvals.ServerAddress = serverAddress;
            return this;
        }
        public IConfigurationBuilder SetPort(string port)
        {
            cvals.Port = port;
            return this;
        }
        public IConfigurationBuilder SetLogin(string input)
        {
            cvals.Login = input;
            return this;
        }
        public IConfigurationBuilder SetPass(string pass)
        {
            cvals.Pass = pass;
            return this;
        }
        public IConfigurationBuilder SetEmailAddress(string email)
        {
            cvals.EmailAddress = email;
            return this;
        }
        public IConfigurationBuilder SetRecipientAddress(string recipientAddress)
        {
            cvals.RecipientAddress = recipientAddress;
            return this;
        }
        public IConfigurationBuilder SetConnectionMethod(bool connectionMethod)
        {
            cvals.ConnectionMethod = connectionMethod.ToString();
            return this;
        }
        public IConfigurationBuilder SetSslTsl(bool sslTsl)
        {
            cvals.SslTsl = sslTsl.ToString();
            return this;
        }
        public IConfigurationBuilder SetImage(string image)
        {
            cvals.Image = image.ToString();
            return this;
        }
        public SettingTextBoxes Build() => cvals;
        public static ConfigValuesBuilder Create() => new ConfigValuesBuilder();
    }
}
