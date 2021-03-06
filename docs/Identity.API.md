## Identity.API
Provides authorization for users. \
If credetials were correct, user receives token which can be used in the future. \
Uses own grant, which is combination of implicit and client credentials. \
Receives client_secret, scope, login and password.
## Nuget packages
 - IdentityServer4
 - IdentityServer.LdapExtensions
 - EntityFrameworkCore
 - Serilog.AspNetCore
 - AspNetCore.Authentication.JwtBearer
 - AspNetCore.Identity
 - Newtonsoft.Json
 
## Usage
Like Main.API, uses appsetings.json
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
  "Urls": "https://localhost:6001",
  "Authority":  "https://127.0.0.1:6001", 
  "AllowedHosts": "*",
  "ConnectionString": "Data Source=SOURCE;Initial Catalog=MyIssueIdentityDB;Integrated Security=False;Persist Security Info=True;User ID=sa;Password=1234;TrustServerCertificate=True",
  "LDAPEnabled": false,
  "LDAP": {
    "Connections": [
      {
        "Url": "localhost",
        "Port": 389,
        "BindDn": "cn=,dc=",
        "BindCredentials": "Pass",
        "SearchBase": "ou=,DC=,dc=",
        "SearchFilter": "",
        "PreFilterRegex": ""
      }
    ]

  },
  "IdentityServer": {
    "IssuerUri": "urn:localhost.com",
    "Authority": "https://localhost.com",
    "Issuer": "localhost.com",
    "Secret": "secret",
    "ApiScopes": [
      {
        "Name": "MainScope"
      },
      {
        "Name": "server_api"
      },
      {
        "Name": "server_console"
      }
    ],
    "ApiResources": [
      {
        "Name": "resource1",
        "UserClaims": [
          "sub",
          "name"
        ],
        "Scopes": [
          "MainScope"
        ],
        "DisplayName": "Main Resource"
      },
      {
        "Name": "server_api",
        "Scopes": [
          "server_api"
        ],
        "DisplayName": "Server API"
      },
      {
        "Name": "server_console",
        "Scopes": [
          "server_console"
        ],
        "DisplayName": "Server Console"
      },
      {
        "Name": "web_ui",
        "Scopes": [
          "server_api"
        ],
        "DisplayName": "Web UI"
      }
    ],
    "Clients": [
      {
        "ClientId": "server1",
        "ClientName": "Server1",
        "ClientSecrets": [
          {
            "Value": "w7Johiu2r4I3AhRKUrOaHDHdVEG5aNa179uSXW719m0=" //sha256 client secret
          }
        ],
        "IdentityTokenLifetime": 86400,
        "AllowedGrantTypes": [ "my_issue_granttype" ],
        "AllowedScopes": [ "server_api" ]
      },
      {
        "ClientId": "mainapiswaggerui",
        "ClientName": "Main API Swagger UI",
        "AllowedGrantTypes": [ "implicit" ],
        "AllowedScopes": [ "server_api" ],
        "RedirectUris": [
          "https://localhost:5004/swagger/oauth2-redirect.html"
        ],
        "PostLogoutRedirectUris": [
          "https://localhost:5004/swagger/"
        ],
        "AllowAccessTokensViaBrowser": true
      },
      {
        "ClientId": "webui",
        "ClientName": "Web UI",
        "AllowedGrantTypes": [ "implicit" ],
        "AllowedScopes": [ "server_api" ],
        "RedirectUris": [
          "https://localhost:5001/nav-menu-logged/"
        ],
        "PostLogoutRedirectUris": [
          "https://localhost:5001/nav-menu/"
        ],
        "AllowAccessTokensViaBrowser": true
      }
    ]
  }
  /*
  "Token": {
    "Secret": "USED TO SIGN AND VERIFY JWT TOKEN",
    "Issuer": "issuer",
    "Audience": "audience"
  }*/

}
```