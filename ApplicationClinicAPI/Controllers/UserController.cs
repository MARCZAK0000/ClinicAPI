using ApplicationClinicAPI.Entities;
using ApplicationClinicAPI.Model.CreateAccount;
using ApplicationClinicAPI.Model.LoginAccount;
using ApplicationClinicAPI.Services.ServicesUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ApplicationClinicAPI.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }


        [HttpPost("register")]
        public async Task<ActionResult> CreateAccount(CreateAcc create)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _userServices.CreateAccount(create);

            var createdResource = new { Good = 1, Version = "1.0" };
            return Created( string.Empty, createdResource);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDto login)
        {
            string token = await _userServices.Login(login);


            return Ok(token);   
        }

        [HttpGet("informations")]
        [Authorize]
        public async Task<ActionResult> GetInfomrations()
        {

            var result = await _userServices.GetInformations();

            return Ok(result); 


        }
    }
}
