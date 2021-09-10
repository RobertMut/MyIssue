using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyIssue.Core.Constants;
using MyIssue.Core.String;

namespace MyIssue.Core.Commands
{
    public class User
    {
        public static IEnumerable<byte[]> Login(string login, string pass)
        {
            return new List<byte[]>()
            {
                StringStatic.ByteMessage(UserLogin.command),
                StringStatic.ByteMessage(string.Format(UserLogin.loginParameters, login, pass))

            };
        }

        public static IEnumerable<byte[]> Logout()
        {
            return new List<byte[]>()
            {
                StringStatic.ByteMessage(UserLogin.logout)
            };
        }
    }
}
