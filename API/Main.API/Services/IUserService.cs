using System.Collections.Generic;
using MyIssue.Core.DataTransferObjects.Request;
using MyIssue.Core.DataTransferObjects.Return;
using MyIssue.Main.API.Model;

namespace MyIssue.Main.API.Services
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