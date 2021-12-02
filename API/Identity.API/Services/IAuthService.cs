using System.Collections.Generic;
using System.Threading.Tasks;
using MyIssue.Core.DataTransferObjects.Request;
using MyIssue.Core.DataTransferObjects.Return;
using MyIssue.Identity.API.Model;

namespace MyIssue.Identity.API.Services
{
    public interface IAuthService
    {
        Task<User> FindByUsernameAsync(string username);
        Task<bool> ValidateCredentialsAsync(User user, string password);
    }
}