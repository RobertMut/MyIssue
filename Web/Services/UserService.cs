using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using MyIssue.Web.Model;

namespace MyIssue.Web.Services
{
    public class UserService
    {
        public UserService()
        {
        }

        public User GenerateToken(AuthRequest model)
        {
            IEnumerable<byte[]> cmds = new List<byte[]>()
                .Concat(Core.Commands.User.Login(model.Login, model.Password))
                .
        }
    }
}
