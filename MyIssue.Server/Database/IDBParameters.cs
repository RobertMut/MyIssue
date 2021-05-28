using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIssue.Server.Database
{
    public interface IDBParameters
    {
        DBParametersTemplate Parameters(IDBParametersBuilder _builder, string database, string address, string username, string password, string taskTable, string userTable, string employeesTable);
    }
}
