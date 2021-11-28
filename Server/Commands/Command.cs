using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using IdentityModel;
using IdentityModel.Client;
using IdentityServer4.Models;
using MyIssue.Infrastructure.Model;
using MyIssue.Server.Model;
using MyIssue.Server.Net;
using MyIssue.Server.Services;
using Org.BouncyCastle.Bcpg;
using Parameters = MyIssue.Server.Model.Parameters;

namespace MyIssue.Server.Commands
{
    public abstract class Command
    {
        protected HttpClient httpclient;
        public Command()
        {
            httpclient = new HttpClient();
            httpclient.BaseAddress = new Uri(Parameters.Api);
            httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
            httpclient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            httpclient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
            httpclient.DefaultRequestHeaders.Connection.Add("keep-alive");

        }

        protected string SetBearerToken(string login, string password)
        {
            bool wait = true;
            string token = string.Empty;
            new Task(async () =>
            {
                token = await GetBearerTokenAsync(login, password);
                wait = false;
            }).Start();
            while (wait)
                Task.Delay(200);
            httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return token;
        }

        public abstract void Invoke(Model.Client client, CancellationToken ct);

        private async Task<string> GetBearerTokenAsync(string login, string password)
        {
            var client = new HttpClient(new HttpClientHandler { ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator });
            client.BaseAddress = new Uri(Parameters.AuthAddress);
            Console.WriteLine("clientsecret".ToSha256());
            var response = await client.RequestTokenAsync(new TokenRequest
            {
                Address = Model.Parameters.AuthAddress + "/connect/token",
                GrantType = "my_issue_granttype",
                ClientId = "server1",
                ClientSecret ="clientsecret",//.ToSha256(),

                Parameters =
                {
                    {"login", login},
                    {"password", password},
                    {"scope", "server_api"}
                }
            });

            return response.AccessToken;
        }
    }
}
