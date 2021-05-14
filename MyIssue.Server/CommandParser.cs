using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

namespace MyIssue.Server
{
	public class CommandParser
	{
        public void Parser(string input, ClientIdentifier client, CancellationToken ct)
		{

			ICommands comm = new Commands();
			string[] command = Splitter(input);
			client.CommandHistory.Add(input);
            
            try
            {
                MethodInfo info = comm.GetType().GetMethod(command[0]);
                //Console.WriteLine(info.Name.ToString());
                if (info is null)
                {
                    Net net = new Connection();
                    net.Write(client.ConnectedSock, "Command not found!\r\n", ct);
                    
                } else if (info.GetParameters().Length.Equals(3))
                {
                    info.Invoke(comm, new Object[] { command[1], client, ct });
                } else if (info.GetParameters().Length.Equals(2))
                {
                    info.Invoke(comm, new Object[] { client, ct });
                }
                
            } catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public string[] Splitter(string input)
		{
			return input.Split(' ');
		}
	}
}


