using System;
using System.Threading;
using MyIssue.Core.Entities;
using MyIssue.Server.Commands;
using MyIssue.Core.Aggregates;
using MyIssue.Core.Exceptions;
using MyIssue.Core.Interfaces;
using System.Reflection;
using System.Linq;

namespace MyIssue.Server
{
    public class CommandParser
    {
        private IAggregateClasses _aggregate;
        private Cmd cmd;
        public CommandParser()
        {
            _aggregate = new AggregateClasses((from t in Assembly.GetExecutingAssembly().GetTypes()
                                              where t.IsClass && t.Namespace == "MyIssue.Server.Commands"
                                              select t).ToList());
        }
        public void Parser(string input, Client client, CancellationToken ct)
        {
            try
            {
                client.CommandHistory.Add(input);
                var type = _aggregate.GetClassByName(input);
                if (type is null) throw new CommandNotFoundException();
                cmd = (Cmd)Activator.CreateInstance(type);
                cmd.Command(client, ct);
            } 
            catch (CommandNotFoundException)
            {
                cmd = new NotFound();
                cmd.Command(client, ct);
            } 
            catch (NotSufficientPermissionsException)
            {
                cmd = new NotSufficient();
                cmd.Command(client, ct);
            }
        }

    }
}


