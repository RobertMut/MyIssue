using System.Collections.Generic;
using MyIssue.API.Model;
using MyIssue.API.Model.Request;
using MyIssue.API.Model.Return;

namespace MyIssue.API.Services
{
    public interface IUserService
    {
        Authenticate AuthenticateUser(AuthRequest model);
        bool VerifyToken(string token);
        string GetClaim(string token, string claimType);
        IEnumerable<User> GetAll();
        User GetByLogin(string login);
    }
}