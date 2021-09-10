﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MyIssue.Core.Exceptions;
using MyIssue.Server.Model;
using MyIssue.Server.Net;

namespace MyIssue.Server.Commands
{
    public class GetTask : Command
    {
        public static string Name = "GetTask";
        public override void Invoke(Model.Client client, CancellationToken ct)
        {

            if (!client.Status.Equals(1)) throw new NotSufficientPermissionsException();
            LogUser.TypedCommand("Get", "Executed", client);
            NetWrite.Write(client.ConnectedSock, "GET\r\n", ct);
            client.CommandHistory.Add(NetRead.Receive(client.ConnectedSock, ct).Result);
            HttpResponseMessage httpresponse = httpclient.GetAsync("api/Tasks/").GetAwaiter().GetResult();
            string response = httpresponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            Console.WriteLine(response);
            NetWrite.Write(client.ConnectedSock,response,ct);
            // unit.UserRepository.Add(new User
            // {
            //     UserLogin = login,
            //     Password = pass,
            //     UserType = type
            // });
            // unit.Complete();

        }
    }
}