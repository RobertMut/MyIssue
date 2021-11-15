using System.Collections.Generic;
using MyIssue.Core.DataTransferObjects.Request;
using MyIssue.Core.DataTransferObjects.Return;
using MyIssue.Identity.API.Model;

namespace MyIssue.Identity.API.Services
{
    public interface IAuthService
    {
        Authenticate AuthenticateUser(AuthRequest model);
        bool VerifyToken(string token);
        string GetClaim(string token, string claimType);
        IEnumerable<User> GetAll();
        User GetByLogin(string login);
        string RevokeToken(string token);
    }
}