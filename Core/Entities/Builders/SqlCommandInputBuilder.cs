using System;
using System.Collections.Generic;
using System.Linq;
using MyIssue.Core.Interfaces;
using MyIssue.Core.String;
namespace MyIssue.Core.Entities.Builders
{
    public class SqlCommandInputBuilder : ISqlCommandInputBuilder
    {
        private SqlCommandInput sqlCommand;
        private IStringTools _tools;

        private SqlCommandInputBuilder()
        {
            sqlCommand = new SqlCommandInput();
            _tools = new StringTools();
        }
        public ISqlCommandInputBuilder SetCommandFromImap(string subject, string body, int hashCode, DateTime date)
        {
            var email = _tools.SplitBrackets(subject, '[', ']').Where(x => !string.IsNullOrEmpty(x)).ToArray();
            sqlCommand.Command = new string[]{
                email[4],
                string.Format("{0} {1}\n{2}", email[2], email[3], string.IsNullOrWhiteSpace(body) ? "No description.." : body),
                date.ToString(),
                email[1],
                "1",
                hashCode.ToString()
            };
            return this;
        }
        public ISqlCommandInputBuilder SetCommandFromArray(IEnumerable<string> input)
        {
            sqlCommand.Command = StringStatic.CommandSplitter(input.ToList()[input.Count() - 1], "\r\n<NEXT>\r\n");
            return this;
        }
        public ISqlCommandInputBuilder SetTable(string table)
        {
            sqlCommand.Table = table;
            return this;
        }
        public SqlCommandInput Build() => sqlCommand;
        public static SqlCommandInputBuilder Create() => new SqlCommandInputBuilder();
    }

}
