using System;

namespace MyIssue.Identity.API.Auth.Configuration
{
    public class AuthenticationOptions
    {
        public TimeSpan TokenLifetime { get; set; } = TimeSpan.FromDays(1);

        public bool AllowRefresh { get; set; } = true;
    }
}