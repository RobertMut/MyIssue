using MyIssue.Core.Commands;

namespace MyIssue.Core.Model.Return
{
    public class Authenticate
    {
        public string Login { get; set; }
        public decimal Type { get; set; }
        public string Token { get; set; }

        public Authenticate(string userLogin, decimal type, string token)
        {
            Login = userLogin;
            Type = type;
            Token = token;
        }
    }
}
