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
                StringStatic.ByteMessage(UserLogin.login),
                StringStatic.ByteMessage(string.Format(UserLogin.loginParameters, login, pass))

            };
        }
        public static IEnumerable<byte[]> TokenLogin(string login, string token)
        {
            return new List<byte[]>()
            {
                StringStatic.ByteMessage(UserLogin.tokenLogin),
                StringStatic.ByteMessage(string.Format(UserLogin.loginParameters, login, token))

            };
        }

        public static IEnumerable<byte[]> RevokeLogout(string token)
        {
            return new List<byte[]>()
            {
                StringStatic.ByteMessage(UserLogin.revokeLogout),
                StringStatic.ByteMessage($"{token}\r\n<EOF>\r\n")
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
