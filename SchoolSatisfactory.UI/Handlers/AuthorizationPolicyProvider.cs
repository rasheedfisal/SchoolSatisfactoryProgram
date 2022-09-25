using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace SchoolSatisfactory.UI.Handlers
{
    public class AuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
	{
		private readonly AuthorizationOptions _options;

		public AuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
		{
			_options = options.Value;
		}

		public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
		{
			AuthorizationPolicy policy = await base.GetPolicyAsync(policyName) ?? new AuthorizationPolicyBuilder().AddRequirements(new AuthorizationRequirement(policyName)).Build();
			//var testPolicy = new AuthorizationPolicyBuilder().AddRequirements(new AuthorizationRequirement(policyName)).Build();
			return policy;
		}
	}
}
