using MyIssue.Core.Entities;
using MyIssue.Core.Entities.Builders;
using MyIssue.Core.Exceptions;
using MyIssue.Infrastructure.Files;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Xml.Linq;

namespace MyIssue.Server
{
    public static class Initializer
    {

        public static void InitializeParameters(XDocument config)
        {
            if (!File.Exists("configuration.xml")) throw new ConfigurationNotFoundException("Configuration file does not exist");
            Parameters.BufferSize = Convert.ToInt32(ConfigValue.GetValue("bufferSize", config));
            Parameters.Timeout = Convert.ToInt32(ConfigValue.GetValue("timeout", config));
            ImapParameters.Parameters = ImapParametersBuilder
                .Create()
                    .SetAddress(ConfigValue.GetValue("i_address", config))
                    .SetPort(Convert.ToInt32(ConfigValue.GetValue("i_port", config)))
                    .SetSocketOptions(
                        (MailKit.Security.SecureSocketOptions)Enum.Parse(
                            typeof(MailKit.Security.SecureSocketOptions), ConfigValue.GetValue("i_connectionOptions", config)))
                    .SetLogin(ConfigValue.GetValue("i_login", config))
                    .SetPassword(ConfigValue.GetValue("i_password", config))
                .Build();
            DBParameters.Parameters = DBParametersBuilder
                .Create()
                    .SetDBAddress(ConfigValue.GetValue("d_address", config))
                    .SetDatabase(ConfigValue.GetValue("d_database", config))
                    .SetUsername(ConfigValue.GetValue("d_username", config))
                    .SetPassword(ConfigValue.GetValue("d_password", config))
                    .SetEmployeesTable(ConfigValue.GetValue("d_employeesTable", config))
                    .SetUsersTable(ConfigValue.GetValue("d_usersTable", config))
                    .SetTaskTable(ConfigValue.GetValue("d_taskTable", config))
                    .SetClientsTable(ConfigValue.GetValue("d_clientsTable", config))
                .Build();

            DBParameters.ConnectionString = new SqlConnectionStringBuilder()
            {
                DataSource = DBParameters.Parameters.DBAddress,
                UserID = DBParameters.Parameters.Username,
                Password = DBParameters.Parameters.Password,
                InitialCatalog = DBParameters.Parameters.Database
            };
            ClientCounter.Clients = 0;
        }
    }
}
