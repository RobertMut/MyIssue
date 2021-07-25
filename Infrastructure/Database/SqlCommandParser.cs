using System;
using MyIssue.Core.Entities;
using MyIssue.Infrastructure.Database.SqlCommands;
using MyIssue.Core.Aggregates;
using MyIssue.Core.Exceptions;
using System.Data.SqlClient;
using MyIssue.Core.Interfaces;

namespace MyIssue.Infrastructure.Database
{
    public class SqlCommandParser : ISqlCommandParser
    {
        private IAggregateClasses _aggregate;
        private SqlCmd sqlCmd;
        public SqlCommandParser()
        {
            _aggregate = new AggregateClasses();
            _aggregate.SetTypes(_aggregate.GetAllClassTypes("Infrastructure", "MyIssue.Infrastructure.Database.SqlCommands"));
        }
        public SqlCommand SqlCmdParser(string name, SqlCommandInput query)
        {
            try
            {
                var type = _aggregate.GetClassByName(name);
                if (type is null) throw new CommandNotFoundException();
                sqlCmd = (SqlCmd)Activator.CreateInstance(type);
                return sqlCmd.SqlCommand(query);
            }
            catch (CommandNotFoundException)
            {
                return null;
            }
        }

    }
}


