using Microsoft.AspNetCore.Authorization;

namespace JustPlay.Authorization
{
    public class MustBeAdminRequirement : IAuthorizationRequirement
    {
        public MustBeAdminRequirement()
        {
            
        }
    }
}
