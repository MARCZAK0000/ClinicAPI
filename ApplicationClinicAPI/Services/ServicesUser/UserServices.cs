using ApplicationClinicAPI.Authentciation;
using ApplicationClinicAPI.Entities;
using ApplicationClinicAPI.Exceptions;
using ApplicationClinicAPI.Model.CreateAccount;
using ApplicationClinicAPI.Model.LoginAccount;
using ApplicationClinicAPI.Model.UserAccountDto;
using ApplicationClinicAPI.Model.VisitModel;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApplicationClinicAPI.Services.ServicesUser
{
    public class UserServices : IUserServices
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly IUserContextServices _userContextServices;
        private readonly IMapper _mapper;
        private readonly ILogger<UserServices> _logger;
        public UserServices(DatabaseContext databaseContext, IPasswordHasher<User> passwordHasher,
            AuthenticationSettings authenticationSettings, IUserContextServices userContextServices,
            IMapper mapper, ILogger<UserServices> logger)
        {
            _passwordHasher = passwordHasher;
            _databaseContext = databaseContext;
            _authenticationSettings = authenticationSettings;
            _userContextServices = userContextServices;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task CreateAccount(CreateAcc create)
        {
            if (create is null)
            {
                throw new NotFoundException("Not found information in registration request");
            }

            var newUser = new User()
            {
                FirstName = create.FirstName,
                LastName = create.LastName,
                Email = create.Email,
                Pesel = create.Pesel,
                DateOfBirth = create.DateOfBirth,
                DateOfRegistration = create.DateOfRegistration,
                roleID = create.roleId
            };

            
            var hassPassword = _passwordHasher.HashPassword(newUser, create.Password);
            newUser.Password = hassPassword;
            await _databaseContext.AddAsync(newUser);
            await _databaseContext.SaveChangesAsync();
            _logger.LogInformation($"Create account {newUser.Email}");

        }

        public async Task<UserAccDto> GetInformations()
        {
            var user = await _databaseContext.Users
                .Include(r=>r.Visits)
                .ThenInclude(cr=>cr.Doctors)
                .FirstOrDefaultAsync(p=>p.Id == _userContextServices.Id) ?? throw new NotFoundException("Something went wrong");

            var result = new UserAccDto()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
                Pesel = user.Pesel,
                Visits = _mapper.Map<List<VisitsDto>>(user.Visits),
            };

            return result;
        }

        public async Task<string> Login(LoginDto login)
        {
            var user = await _databaseContext.Users
                .Include(r=>r.Role)
                .FirstOrDefaultAsync(u=>u.Email == login.Email) 
                ?? throw new BadRequestException("Invalid email or password");

            var verifyPassword = _passwordHasher.VerifyHashedPassword(user, user.Password, login.Password);

            if (verifyPassword == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid email or password");
            }

            var claim = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Role, user.Role.Name),
                new Claim("DateOfBirth", user.DateOfBirth.ToString("yyyy-MM-dd")),
                new Claim("Pesel",user.Pesel)
            };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));


            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(issuer: _authenticationSettings.JwtIssuer,
                audience: _authenticationSettings.JwtIssuer,
                claims: claim,
                expires: expires,
                signingCredentials: cred);


            var tokenHandler = new JwtSecurityTokenHandler();
            _logger.LogInformation($"Login: {user.Id}");
            return tokenHandler.WriteToken(token);

        }
    }
}
