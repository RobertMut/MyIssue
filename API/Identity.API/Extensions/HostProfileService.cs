using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer.LdapExtension;
using IdentityServer.LdapExtension.UserStore;
using IdentityServer4.Models;
using Microsoft.Extensions.Logging;

namespace MyIssue.Identity.API.Extensions
{
    public class HostProfileService : LdapUserProfileService
    {
        public HostProfileService(ILdapUserStore users, ILogger<LdapUserProfileService> logger) : base(users, logger)
        {
        }

        public override async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            await base.GetProfileDataAsync(context);
            var transaction =
                context.RequestedResources.ParsedScopes.FirstOrDefault(x => x.ParsedName == "transaction");
            if (transaction?.ParsedParameter != null)
            {
                context.IssuedClaims.Add(new Claim("transaction_id", transaction.ParsedParameter));
            }
        }
    }
}
