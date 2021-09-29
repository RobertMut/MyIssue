using System;
using System.Collections.Generic;
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
            var token = headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            return new TokenAuth(token);
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
