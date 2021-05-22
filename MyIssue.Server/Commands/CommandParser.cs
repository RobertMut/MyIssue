﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using MyIssue.Server.Net;
namespace MyIssue.Server
{
    public class CommandParser
    {
        public void Parser(string input,Client client, CancellationToken ct)
        {
            INetwork _net = new Network();
            IDBCommands _dbcmd = new Commands();
            IUserCommands _ucmd = new Commands();
            string[] command = Splitter(input);
            client.CommandHistory.Add(input);
            Console.WriteLine(client.ConnectedSock.RemoteEndPoint);
            
            var method = GetMethod(new Type[] { _dbcmd.GetType(), _ucmd.GetType() }, command[0]);
            
            if (method.Item2 is null)
            {
                _net.Write(client.ConnectedSock, "Command not found!\r\n", ct);

            }
            else if (method.Item2.GetParameters().Length.Equals(3))
            {
                method.Item2.Invoke(Activator.CreateInstance(method.Item1),
                    new Object[] { command[1], client, ct });
            }
            else if (method.Item2.GetParameters().Length.Equals(2))
            {
                method.Item2.Invoke(Activator.CreateInstance(method.Item1),
                    new Object[] {client, ct });
            }
        }

        private Tuple<Type, MethodInfo> GetMethod(Type[] type, string command)
        {
            MethodInfo miRes = null;
            Type tRes = null;

            foreach (var t in type)
            {
                var met = t.GetMethod(command);
                if (!(met is null))
                {
                    miRes = met;
                    tRes = t;
                }
            }
            return Tuple.Create(tRes, miRes);
        }

        public string[] Splitter(string input)
        {
            return input.Split(' ');
        }
    }
}

