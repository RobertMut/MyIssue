## Server
"Bridge" between APIs and client application such as Web, or DesktopApp. \
Works using set of commands, which were parsed with every query send. \
Effect is similar to [SMTP server](https://datatracker.ietf.org/doc/html/rfc5321#section-2.3.7). \
Command arguments are separated by `\r\n<NEXT>\r\n` and ended with `\r\n<EOF>\r\n`. \
Receives data through commands or connects to imap and listens for emails in specific format. \
## Nuget packages
 - IdentityModel
 - Newtonsoft.Json
 
## Usage
Server stores and generates values inside configuration.xml file.
```xml
{<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <imapSettings>
    <i_enabled>true</i_enabled>
    <i_address>127.0.0.1</i_address>
    <i_port>143</i_port>
    <i_connectionOptions>Auto</i_connectionOptions>
    <i_login>login</i_login>
    <i_password>pass</i_password>
  </imapSettings>
  <apiSettings>
    <api_address>DESKTOP</api_address>
    <authaddress>IdentityAuthAddress</authaddress>
    <api_login>Login</api_login>
    <api_password>Pass</api_password>
  </apiSettings>
  <serverSettings>
    <enabled>true</enabled>
    <listenAddress>127.0.0.1</listenAddress>
    <port>49153</port>
    <bufferSize>16553</bufferSize>
    <timeout>10000</timeout>
  </serverSettings>
</configuration>
```