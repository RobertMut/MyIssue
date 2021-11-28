using System;
using System.Threading.Tasks;
using IdentityServer4.Validation;
using Microsoft.Extensions.Logging;
using MyIssue.Identity.API.Services;

namespace MyIssue.Identity.API.Auth.GrantValidators
{
    public class MyIssueGrantValidator : IExtensionGrantValidator
    {
        private readonly IAuthService _auth;
        private readonly ILogger<MyIssueGrantValidator> _logger;
        public string GrantType => "my_issue_granttype";

        public MyIssueGrantValidator(IAuthService authService, ILogger<MyIssueGrantValidator> logger)
        {
            _auth = authService ?? throw new ArgumentNullException(nameof(authService));
            _logger = logger;
        }

        public async Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            var login = context.Request.Raw.Get("login");
            var password = context.Request.Raw.Get("password");

            var user = await _auth.FindByUsernameAsync(login);
            if (user != null)
            {
                if (await _auth.ValidateCredentialsAsync(user, password))
                {
                    context.Result = new GrantValidationResult(user.UserLogin, GrantType, DateTime.Now);
                    _logger?.LogInformation($"{login} succeeded to authenticate with {GrantType} grant");
                }
            }
            _logger?.LogInformation($"{login} failed to authenticate with {GrantType} grant");
        }
    }
}