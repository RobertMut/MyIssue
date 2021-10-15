using System.Security.Cryptography.X509Certificates;
using MyIssue.Web.Helpers;

namespace MyIssue.Web.Model
{
    public class TokenAuth
    {
        public string Login { get; set; }
        public string Token { get; set; }

        public TokenAuth()
        {
        }

        public TokenAuth(string login, string token)
        {
            Login = login;
            Token = token;
        }

        public TokenAuth(string token)
        {
            Login = TokenHelper.GetClaim(token, "username").Result;
            Token = token;
        }
    }
}