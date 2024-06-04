using System.Security.Claims;

namespace ApplicationClinicAPI.Services.ServicesVisits
{
    public interface IVisitContextServices
    {
        int? ID { get; }

        ClaimsPrincipal? User { get; }

    }

    public class VisitContextServices : IVisitContextServices
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public VisitContextServices(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public ClaimsPrincipal? User => _contextAccessor.HttpContext?.User;

        public int? ID => User is null ? null : int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
    }
}