## Introduction
This repository contains API, Web, Server and WPF application to create and track issues made by client.
## Description
This repository consist of four parts:
 - [Main.API](./docs/Main.API.md). - provides required data from database,
 - [Identity.API](./docs/Identity.API.md) - used to authenticate user,
 - [Server](./docs/Server.md) - "bridge" between API and Web/DesktopApp, also connects to imap and search for new mails,
 - [Web](./docs/Web.md) - contains panel to create task, create new client and manage tasks,
 - [DesktopAPP](./docs/DesktopAPP.md) - just WPF app to send and crate new task.
 
## Usage

 First you have to clone repository.\
 If you are using IIS you have to use its configuration to set up address. \
Otherwise address for your application should be set inside json file.
 ### API
 Set following values in API's appsettings.json:\
 Example:
 ```json
 "ConnectionString": "Data Source=<DATABASE ADDRESS>;Initial Catalog=<DATABASE>;Integrated Security=False;Persist Security Info=True;User ID=<LOGIN>;Password=<PASSWORD>;TrustServerCertificate=True"
 ```
 ```json
   "Token": {
    "Secret": "USED TO SIGN AND VERIFY JWT TOKEN",
    "Issuer": "issuer",
    "Audience": "audience"
```
```json
  "API": {
    "applicationUrl": "https://URL1:PORT;http://URL2:PORT;..."
  } 
```
### Server
You have to provide following values in configuration.xml file in server directory:
```xml
<configuration>
  <imapSettings> <!-- Used to login and check emails -->
    <i_enabled>true</i_enabled>
    <i_address>127.0.0.1</i_address>
    <i_port>143</i_port>
    <i_connectionOptions>Auto</i_connectionOptions>
    <i_login>login</i_login>
    <i_password>pass</i_password>
  </imapSettings>
  <apiSettings> <!-- Used to send http requests to API -->
    <api_address>DESKTOP</api_address>
    <api_login>Login</api_login>
    <api_password>Pass</api_password>
  </apiSettings>
  <serverSettings> <!-- Basic server configuration -->
    <enabled>true</enabled>
    <listenAddress>127.0.0.1</listenAddress>
    <port>49153</port>
    <bufferSize>16553</bufferSize>
    <timeout>10000</timeout>
  </serverSettings>
</configuration>
```
### Web
Contains appsettings.json like API, but few values are different
```json
  "ServerConnection": {
    "ServerIp": "127.0.0.1",
    "Port": "49153",
  }
  ```
```json
  "Web": {
    "applicationUrl": "https://URL1:PORT;http://URL2:PORT;..."
  }
```
### DesktopApp
Setting up application is easier in contrary to API or Server. When you open application you will be welcomed by configuration screen \
![Configuration Screen](https://i.imgur.com/yEaPMmj.png) \
Configuration and log files should appear in %appdata%\MyIssue\

### Notes
When you are creating task, you should have client already added in database. Company name must be the same as in database.\
Default user:
```
Login: Admin
Password: 1234
```
Default client: MyIssue
