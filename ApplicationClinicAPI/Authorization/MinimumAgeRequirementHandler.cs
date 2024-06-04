using Microsoft.AspNetCore.Authorization;

namespace ApplicationClinicAPI.Authorization
{
    public class MinimumAgeRequirementHandler : AuthorizationHandler<MinimumAgeRequirement>
    {
        private readonly ILogger _logger;
        public MinimumAgeRequirementHandler(ILogger<MinimumAgeRequirement> logger) 
        {
            _logger = logger;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
        {
            var dateOfBirth = DateTime.Parse(context.User.FindFirst(c => c.Type == "DateOfBirth").Value);
            if(dateOfBirth.AddYears(requirement.MinimumAge) < DateTime.Today)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
            
        }
    }
}
