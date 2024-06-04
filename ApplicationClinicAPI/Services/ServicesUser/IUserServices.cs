using ApplicationClinicAPI.Model.CreateAccount;
using ApplicationClinicAPI.Model.LoginAccount;
using ApplicationClinicAPI.Model.UserAccountDto;
using System.Security.Claims;

namespace ApplicationClinicAPI.Services.ServicesUser
{
    public interface IUserServices
    {
        Task CreateAccount(CreateAcc create);

        Task <string> Login(LoginDto login);

        Task<UserAccDto> GetInformations();
    }
}