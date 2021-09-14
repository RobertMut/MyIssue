using System.Threading.Tasks;

namespace MyIssue.Web.Services
{
    public interface IUserService
    {
        Task<string> GenerateToken(string login, string password);
        Task<string> GetClaim(string token, string claimType);
        Task<bool> ValidateToken(string login, string token);
        Task<string> RevokeToken(string token);
    }
}