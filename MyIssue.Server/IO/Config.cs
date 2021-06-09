﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIssue.Server.IO
{
    public class Config
    {
        public const string emptyConfig =
@"<?xml version=""1.0"" encoding=""utf-8"" ?>
<configuration>
  <imapSettings>
    <i_enabled>true</i_enabled>
    <i_address>127.0.0.1</i_address>
    <i_port>143</i_port>
    <i_connectionOptions>Auto</i_connectionOptions>
    <i_login>login</i_login>
    <i_password>pass</i_password>
  </imapSettings>
  <databaseSettings>
    <d_address>DESTKOP</d_address>
    <d_database>DATABASE</d_database>
    <d_username>username</d_username>
    <d_password>password</d_password>
    <d_employeesTable>dbo.EMPLOYEES</d_employeesTable>
    <d_usersTable>dbo.USERS</d_usersTable>
    <d_taskTable>dbo.TASKS</d_taskTable>
    <d_clientsTable>dbo.CLIENTS</d_clientsTable>
  </databaseSettings>
  <serverSettings>
    <enabled>true</enabled>
    <listenAddress>127.0.0.1</listenAddress>
    <port>49153</port>
    <bufferSize>16553</bufferSize>
    <timeout>10000</timeout>
  </serverSettings>
</configuration>";
    }
}
