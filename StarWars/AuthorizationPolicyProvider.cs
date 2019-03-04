using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace StarWars
{
    public class AuthorizationPolicyProvider : IAuthorizationPolicyProvider
    {
        public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            throw new System.NotImplementedException();
        }

        public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
