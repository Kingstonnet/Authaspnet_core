using Microsoft.AspNetCore.Authorization;

namespace Webapp_underthehood.Authorization
{
    public class HRManagerProbationRequirement:IAuthorizationRequirement
    {
        public int Probabtionmaonths { get; }
        public HRManagerProbationRequirement(int probabtionmaonths)
        {
            Probabtionmaonths = probabtionmaonths;
        }

        
    }

    public class HRManagerProbationRequirementHandler : AuthorizationHandler<HRManagerProbationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HRManagerProbationRequirement requirement)
        {
            if(!context.User.HasClaim(x=>x.Type== "empdate"))
            {
                return Task.CompletedTask;
            }

          if(DateTime.TryParse(context.User.FindFirst(x=>x.Type=="empdate")? .Value,out DateTime empdate))
            {
                var period = DateTime.Now - empdate;
                if (period.Days > 30 * requirement.Probabtionmaonths)
                    context.Succeed(requirement);
            }

          return Task.CompletedTask;
        }
    }
}
