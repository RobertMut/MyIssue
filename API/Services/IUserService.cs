using System.Collections.Generic;
using MyIssue.API.Model;
using MyIssue.Core.Model.Request;
using MyIssue.Core.Model.Return;

namespace MyIssue.API.Services
{
    public interface IUserService
    {
        Authenticate AuthenticateUser(AuthRequest model);
        bool VerifyToken(string token);
        string GetClaim(string token, string claimType);
        IEnumerable<User> GetAll();
        User GetByLogin(string login);
        string RevokeToken(string token);
    }
}