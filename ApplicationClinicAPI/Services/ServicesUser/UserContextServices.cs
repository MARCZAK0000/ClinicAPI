using System.Security.Claims;

namespace ApplicationClinicAPI.Services.ServicesUser
{
    public class UserContextServices : IUserContextServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextServices(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;

        public int? Id => User is null ? null : int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);


    }
}
