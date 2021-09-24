using System.Threading.Tasks;
using MyIssue.Web.Model;

namespace MyIssue.Web.Services
{
    public interface IUsersService
    {
        Task<UsersRoot> GetUsers(string? username, TokenAuth model);
    }
}