using FluentValidation;

namespace ApplicationClinicAPI.Model.LoginAccount
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(prop => prop.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(50);
            RuleFor(prop=>prop.Password)
                .NotEmpty()
                .MaximumLength(18)
                .MinimumLength(6);
        }
    }
}
