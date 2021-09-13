using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyIssue.API.Model.Request
{
    public class AuthTokenRequest
    {
        public string Username { get; set; }
        public string Token { get; set; }
    }
}
