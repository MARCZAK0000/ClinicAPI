using Microsoft.AspNetCore.Authorization;

namespace ApplicationClinicAPI.Authorization
{
    public class MinimumAgeRequirement: IAuthorizationRequirement
    {
        public int MinimumAge;
        public MinimumAgeRequirement(int minimumAge)
        {
            MinimumAge = minimumAge;

        }
    }
}
