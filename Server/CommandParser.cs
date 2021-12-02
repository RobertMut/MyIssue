using System;
using System.Threading;
using MyIssue.Server.Commands;
using MyIssue.Core.Aggregates;
using MyIssue.Core.Exceptions;
using MyIssue.Core.Interfaces;
using System.Reflection;
using System.Linq;
using System.Reflection.Emit;
using MyIssue.Server.Services;

namespace MyIssue.Server
{
    public class CommandParser
    {
        private IAggregateClasses _aggregate;
        private Command cmd;
        public CommandParser()
        {
            _aggregate = new AggregateClasses((from t in Assembly.GetExecutingAssembly().GetTypes()
                                              where t.IsSubclassOf(typeof(Command)) && t.Namespace == "MyIssue.Server.Commands"
                                              select t).ToList());
        }
        public void Parser(string input, Model.Client client, CancellationToken ct)
        {
            try
            {
                client.CommandHistory.Add(input);
                var type = (from c in _aggregate.GetAggregatedClasses()
                            where (string)c?.GetField("Name")?.GetValue(null) == input
                            select c)?.FirstOrDefault();
                if (type is null) throw new CommandNotFoundException();
                var objserv = new ObjectService(type);
                cmd = (Command)objserv.CreateInstance();
                cmd.Invoke(client, ct);

            } 
            catch (CommandNotFoundException)
            {
                Console.WriteLine("NOT FOUND");
                //commandDelegate =Delegate.CreateDelegate(typeof(NotFound), typeof(NotFound).GetMethod("Invoke"));
                //commandDelegate.DynamicInvoke(client, ct);
            } 
            catch (NotSufficientPermissionsException)
            {
                //commandDelegate = Delegate.CreateDelegate(typeof(NotSufficient), typeof(NotSufficient).GetMethod("Invoke"));
                //commandDelegate.DynamicInvoke(client, ct);
            }
        }
    }
}


