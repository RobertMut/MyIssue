using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyIssue.API.Infrastructure;
using MyIssue.API.Model;
using MyIssue.Core.DataTransferObjects.Request;
using MyIssue.Core.DataTransferObjects.Return;

namespace MyIssue.API.Services
{
    public class UserService : IUserService
    {
        private readonly MyIssueContext _context;
        private readonly IConfiguration _configuration;
        public UserService(MyIssueContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public Authenticate AuthenticateUser(AuthRequest model)
        {
            var user = _context.Users.SingleOrDefault(
                x => x.UserLogin == model.Username && x.Password == model.Password);
            if (user is null) return null;
            string token = GenerateJwtToken(user, DateTime.Now.AddDays(1));
            return new Authenticate(user.UserLogin, user.UserType, token);
        }

        public bool VerifyToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = _configuration.GetValue<string>("Token:Issuer"),
                    ValidAudience = _configuration.GetValue<string>("Token:Audience"),
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.ASCII.GetBytes(
                            _configuration.GetValue<string>("Token:Secret")
                        ))
                }, out SecurityToken validated);
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}\r\n{e.StackTrace}");
                return false;
            }

            return true;
        }

        public string RevokeToken(string token)
        {
            try
            {
                string username = GetClaim(token, "username");
                var user = _context.Users.FirstOrDefault(user => user.UserLogin.Equals(username));
                if (user is not null) return GenerateJwtToken(user, DateTime.Now.AddMinutes(-1));
            }
            catch (NullReferenceException)
            { }

            return null;
        }
        public string GetClaim(string token, string claimType)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.ReadToken(token) as JwtSecurityToken;
                var stringClaim = securityToken.Claims.First(claim => claim.Type == claimType).Value;
                return stringClaim;
            }
            catch (ArgumentException)
            {
                return null;
            }

        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public User GetByLogin(string login)
        {
            return _context.Users.FirstOrDefault(x => x.UserLogin.Equals(login));
        }
        private string GenerateJwtToken(User user, DateTime howLong)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Token:Secret"]);
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim("username", user.UserLogin));
            claims.AddClaim(new Claim("usertype", user.UserType.ToString()));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _configuration["Token:Audience"],
                Issuer = _configuration["Token:Issuer"],
                Expires = howLong,
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Subject = claims
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
