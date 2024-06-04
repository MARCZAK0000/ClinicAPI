using FluentValidation;

namespace ApplicationClinicAPI.Model.DoctorsModel
{
    public class DoctorsDtoValidator :AbstractValidator<DoctorsDto>
    {
        public DoctorsDtoValidator()
        {
            RuleFor(prop => prop.FirstName)
                .MaximumLength(50)
                .NotEmpty();
            RuleFor(prop => prop.LastName)
                .MaximumLength(50)
                .NotEmpty(); 
            RuleFor(prop => prop.Specialization)
                .MaximumLength(50)
                .NotEmpty(); 
            RuleFor(prop => prop.Title)
                .MaximumLength(20)
                .NotEmpty();
            
        }
    }
}
