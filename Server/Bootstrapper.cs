using MyIssue.Core.Exceptions;
using MyIssue.Infrastructure.Files;
using System.Data.SqlClient;
using System.IO;
using System.Xml.Linq;
using MyIssue.Infrastructure.Model;
using MyIssue.Infrastructure.Model.Builders;
using MyIssue.Server.Client;
using ImapParameters = MyIssue.Infrastructure.Model.ImapParameters;
using Parameters = MyIssue.Server.Model.Parameters;

namespace MyIssue.Server
{
    public static class Bootstrapper
    {

        public static void InitializeParameters(XDocument config)
        {
            if (!File.Exists("configuration.xml")) throw new ConfigurationNotFoundException("Configuration file does not exist");
            Parameters.BufferSize = ConfigValue.GetValue<int>("bufferSize", config);
            Parameters.Timeout = ConfigValue.GetValue<int>("timeout", config);
            Parameters.Api = ConfigValue.GetValue<string>("api_address", config);
            Parameters.Login = ConfigValue.GetValue<string>("api_login", config);
            Parameters.Password = ConfigValue.GetValue<string>("api_password", config);
            Parameters.AuthAddress = ConfigValue.GetValue<string>("auth_address", config);
            ImapParameters.Parameters = ImapParametersBuilder
                .Create()
                    .SetAddress(ConfigValue.GetValue<string>("i_address", config))
                    .SetPort(ConfigValue.GetValue<int>("i_port", config))
                    .SetSocketOptions(ConfigValue.GetValue<string>("i_connectionOptions", config))
                    .SetLogin(ConfigValue.GetValue<string>("i_login", config))
                    .SetPassword(ConfigValue.GetValue<string>("i_password", config))
                .Build();

            ClientCounter.Clients = 0;
        }

    }
}
