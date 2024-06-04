using ApplicationClinicAPI.Entities;
using FluentValidation;
using Microsoft.Identity.Client;

namespace ApplicationClinicAPI.Model.CreateAccount
{
    public class CreateAccValidator : AbstractValidator<CreateAcc>
    {
        public CreateAccValidator(DatabaseContext _databaseContext)
        {
            RuleFor(rule=>rule.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(50);

            RuleFor(rule=>rule.FirstName)
                .MaximumLength(25)
                .NotEmpty();

            RuleFor(rule=>rule.LastName)
                .MaximumLength(50)
                .NotEmpty();
            
            RuleFor(rule=>rule.Password)
                .MaximumLength(18)
                .MinimumLength(6)
                .Equal(rule => rule.Password);


            RuleFor(rule => rule.ConfirmPassword)
              .MaximumLength(18)
              .MinimumLength(6);
              

            RuleFor(rule => rule.Pesel)
                .Length(11);

            RuleFor(rule => rule.Email).Custom((value, context) =>
            {
                var emailInUser = _databaseContext.Users.Any(d => d.Email == value);

                if(emailInUser) 
                {
                    context.AddFailure("Eamil is in use");
                }
            });

            RuleFor(rule => rule.DateOfBirth).Custom((value, context) =>
            {
                if(value.AddYears(18)>DateTime.Now)
                {
                    context.AddFailure("You are too young to register on this page");
                }
            });

        }
    }
}
