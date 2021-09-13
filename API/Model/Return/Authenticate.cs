using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyIssue.API.Model.Return
{
    public class Authenticate
    {
        public string Login { get; set; }
        public decimal Type { get; set; }
        public string Token { get; set; }

        public Authenticate(User user, string token)
        {
            Login = user.UserLogin;
            Type = user.UserType;
            Token = token;
        }
    }
}
