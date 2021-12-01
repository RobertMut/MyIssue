## DesktopAPP
Client for end-user. \
Should be run on user's workstation. \
Sends data through server or imap. \
Stores important data in `%AppData%\MyIssue` with encrypted values.
## Nuget packages
 - Prism.Core
 - Prism.Unity
 - Prism.Wpf
 - Unity.Abstractions
 - Unity.Container
 - Xaml.Behaviors.Wpf
 
## Usage
Just run and fill fields in configuration screen. \
![Configuration Screen](https://i.imgur.com/yEaPMmj.png) \

## Stored configuration file
```xml
<?xml version="1.0" encoding="utf-8" standalone="yes"?>
<configuration>
  <applicationPass>encrypted value</applicationPass>
  <companyName>testcomp</companyName>
  <serverAddress>encrypted value</serverAddress>
  <port>encrypted value</port>
  <login>encrypted value</login>
  <pass>encrypted value</pass>
  <emailAddress>encrypted value</emailAddress>
  <recipientAddress>encrypted value</recipientAddress>
  <isSmtp>True</isSmtp>
  <sslTsl>False</sslTsl>
  <image>path to image</image>
</configuration>
```