using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;

namespace QuickServiceWebAPI.Services.Authentication
{
    public class PermissionAuthorizationPolicyProvider
    : DefaultAuthorizationPolicyProvider
    {
        public PermissionAuthorizationPolicyProvider(
            IOptions<AuthorizationOptions> options)
            : base(options)
        {
        }

        public override async Task<AuthorizationPolicy?> GetPolicyAsync(
            string policyName)
        {
            AuthorizationPolicy? policy = await base.GetPolicyAsync(policyName);

            if (policy is not null)
            {
                return policy;
            }

            var listPolicy = Regex.Split(policyName.Trim(), @"[,]+");
            return new AuthorizationPolicyBuilder()
                .AddRequirements(new PermissionRequirement(listPolicy[0], listPolicy[1]))
                .Build();
        }
    }
}
