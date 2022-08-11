using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using JustPlay.Data.Repository;
using JustPlay.Data.Models;
using JustPlay.Controllers;

namespace JustPlay.Authorization
{
    public class MustBeAdminHandler : AuthorizationHandler<MustBeAdminRequirement>
    {
        private readonly IDataRepository _dataRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MustBeAdminHandler(IDataRepository dataRepository, 
            IHttpContextAccessor httpContextAccessor)
        {
            _dataRepository = dataRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        protected async override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, MustBeAdminRequirement requirement)
        {
            if (!context.User.Identity.IsAuthenticated) 
            {
                context.Fail();
                return;
            }

            var userEmail = _httpContextAccessor.HttpContext.User.FindFirst(c => c.Type.Contains("email"))?.Value;      

            var usersController = new UsersController(_dataRepository, _httpContextAccessor);
            var userRetrieved = usersController.GetUserByEmail().Result;
            
            if (userRetrieved.Value.Admin == true)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
        }
    }
}
