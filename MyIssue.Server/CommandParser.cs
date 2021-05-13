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
                Console.WriteLine(info.Name);
                info.Invoke(comm, new Object[] { command[1], client, ct });
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


