using MyIssue.Core.Entities;
using System;
using System.Collections.Generic;

namespace MyIssue.Core.Interfaces
{
    public interface ISqlCommandInputBuilder
    {
        SqlCommandInput Build();
        ISqlCommandInputBuilder SetCommandFromArray(IEnumerable<string> input);
        ISqlCommandInputBuilder SetCommandFromImap(string subject, string body, int hashCode, DateTime date);
        ISqlCommandInputBuilder SetTable(string table);
    }
}