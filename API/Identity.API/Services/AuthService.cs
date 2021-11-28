using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyIssue.Core.DataTransferObjects.Request;
using MyIssue.Core.DataTransferObjects.Return;
using MyIssue.Identity.API.Infrastructure;
using MyIssue.Identity.API.Model;

namespace MyIssue.Identity.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IdentityContext _context;
        private readonly IConfiguration _configuration;
        public AuthService(IdentityContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        public async Task<User> FindByUsernameAsync(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserLogin == username);
            return user;
        }

        public async Task<bool> ValidateCredentialsAsync(User user, string password)
        {
            var returnedUser = await _context.Users.FirstOrDefaultAsync(u => u.UserLogin == user.UserLogin && u.Password == user.Password);
            if (returnedUser != null)
                return true;
            return false;
        }
    }
}
