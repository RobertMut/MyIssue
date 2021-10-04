﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MyIssue.Core.Exceptions;
using MyIssue.Server.Net;

namespace MyIssue.Server.Commands
{
    public class GetEmployee : Command
    {
        public static string Name = "GetEmployee";
        public override void Invoke(Model.Client client, CancellationToken ct)
        {

            if (client.Status.Equals(1)) throw new NotSufficientPermissionsException();
            LogUser.TypedCommand("GetEmployee", "Executed", client);
            NetWrite.Write(client.ConnectedSock, "GET EMPLOYEE\r\n", ct);
            client.CommandHistory.Add(NetRead.Receive(client.ConnectedSock, ct).Result);
            string[] input = SplitToCommand.Get(client.CommandHistory);
            string baseAddress = "api/Employees/";
            if (!input.Length.Equals(0)) baseAddress += input[0];
            using (var request = new HttpRequestMessage(HttpMethod.Get,
                httpclient.BaseAddress + baseAddress))
            {
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
                request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
                request.Headers.Connection.Add("keep-alive");
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", client.Token);
                HttpResponseMessage httpResponse = httpclient.SendAsync(request).Result;
                string response = httpResponse.Content.ReadAsStringAsync().Result;
                NetWrite.Write(client.ConnectedSock, response, ct);
            }
        }
    }
}