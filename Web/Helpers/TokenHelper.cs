using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MyIssue.Web.Model;

namespace MyIssue.Web.Helpers
{
    public class TokenHelper
    {
        public static async Task<TokenAuth> GetTokenFromHeader(IHeaderDictionary headers)
        {
            var token = headers["Authorization"].FirstOrDefault()?.Split(" ");
            return new TokenAuth(token[0], token[1]);
        }
        public static async Task<string> GetClaim(string token, string claimType)
        {
            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.ReadToken(token) as JwtSecurityToken;
            var stringClaim = securityToken.Claims.First(claim => claim.Type == claimType).Value;
            return stringClaim;
        }
    }
}
