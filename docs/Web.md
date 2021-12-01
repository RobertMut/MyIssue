## Web
Purpose of web is to serve as panel for employees. \
Server treats web the same way as client. \
Front is written in Anugular/Typescript with CSS.
## Nuget packages
 - AspNetCore.Authentication.JwtBearer
 - Newtonsoft.Json
 - Serilog.AspNetCore
 - AspNetCore.Mvc.NewtonsoftJson
 
## Usage
Like Main.API, uses appsetings.json
```{
  "Serilog": {
    "WriteTo": [
      {
        "Name": "Console",
        "Args": { "restrictedToMinimumLevel": "Information" }
      }
      /*{
        "Name": "File",
        "Args": {
          //"path": ".\\",
          "restrictedToMinimumLevel": "Error",
          "shared": true
        }
      }*/
    ],
    "Enrich": [ "FromLogContext", "WithMachineName", "WithThreadId" ]
  },
  "AllowedHosts": "*",
  "ServerConnection": {
    "ServerIp": "127.0.0.1",
    "Port": "49153"
  },
  "Web": {
    "applicationUrl": "https://localhost:5001;http://localhost:5002"
  }
}
```