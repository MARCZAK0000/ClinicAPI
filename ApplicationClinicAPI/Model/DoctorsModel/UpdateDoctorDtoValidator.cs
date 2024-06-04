using FluentValidation;

namespace ApplicationClinicAPI.Model.DoctorsModel
{
    public class UpdateDoctorDtoValidator :AbstractValidator<UpdateDoctorDto>
    {
        public UpdateDoctorDtoValidator()
        {
            RuleFor(prop => prop.LastName)
                .MaximumLength(50);
            RuleFor(prop => prop.Specialization)
                .MaximumLength(50);
            RuleFor(prop => prop.Title)
                .MaximumLength(20);
 
        }
    }
}
