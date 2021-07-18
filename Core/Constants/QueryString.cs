using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyIssue.Core.Constants
{
    public static class QueryString
    {
        public const string insertTask = "INSERT INTO {0}(taskTitle, taskDesc, taskCreation, taskClient, taskType, mailId)VALUES(@TITLE, @DESC, @DATE, (SELECT [clientId] FROM {0} WHERE [clientName] = @CLIENT), @TYPE, @MAILID)";
        public const string insertEmployee = "INSERT INTO {0} VALUES(@LOGIN, @NAME, @SURNAME, @NO, @POSITION)";
        public const string insertUser = "INSERT INTO {0} VALUES(@USERLOGIN, @PASSWORD, @TYPE)";
        public const string selectLogin = "SELECT * FROM {0} WHERE \'{1}\' = {0}.userLogin AND {0}.password = \'{2}\'";
        public const string deleteUser = "DELETE FROM {0} WHERE {0}.userLogin = {1}";
        public const string banUser = "UPDATE {0} SET {0}.status = \'3\' WHERE {0}.userLogin = {1}";
        public const string selectClient = "SELECT [clientId] FROM {0} WHERE [clientName] = @CLIENT";
    }
}
