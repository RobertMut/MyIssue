## Main.API
Just API which connects to Identity.API to validate incoming HTTP request with JWTBearer.
## Nuget packages
 - IdentityServer4.AccessTokenValidation
 - AspNetCore.Authentication.JwtBearer
 - EntityFrameworkCore
 - Serilog.AspNetCore
 - Newtonsoft.Json
 
## Usage
Main.API uses appsetings.json.\
```json
{
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
  "ConnectionString": "Data Source=Source;Initial Catalog=MyIssueDB;Integrated Security=False;Persist Security Info=True;User ID=sa;Password=1234;TrustServerCertificate=True",
  "Token": {
    "Secret": "USED TO SIGN AND VERIFY JWT TOKEN",
    "Issuer": "issuer",
    "Audience": "https://127.0.0.1:6001"
  },
  "API": {
    "ApplicationUrl": "https://localhost:5004",
    "Identity": "https://127.0.0.1:6001",
    "RequireHttps": "false"
  },
  "SwaggerOptions": {
    "Description": "MainAPI",
    "UIEndpoint": "v1/swagger.json",
    "ApiInfo": {
      "Title": "MainApi",
      "Version": "v1"
    },
    "OAuth": {
      "ClientId": "mainapiswaggerui",
      "ClientSecret": "",
      "ClientRealm": "",
      "ClientName": "Main Api Swagger UI",
      "Scopes": {
        "server_api": "Server API"
      }
    }
  }

}

```