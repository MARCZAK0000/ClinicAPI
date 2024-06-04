using System.Security.Claims;

namespace ApplicationClinicAPI.Services.ServicesUser
{
    public interface IUserContextServices
    {
        int? Id { get; }
        ClaimsPrincipal? User { get; }
    }
}