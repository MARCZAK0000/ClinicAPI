using FluentValidation;

namespace ApplicationClinicAPI.Model.UpdateVisitDate
{
    public class UpdateVisitValidator : AbstractValidator<UpdateVisitDto>
    {
        public UpdateVisitValidator()
        {
            RuleFor(p=>p.VisitId)
                .NotEmpty()
                .NotNull();


            RuleFor(p=>p.NewDate)
                .NotNull()
                .Custom((value, context)=> 
                {
                    var date = value;
                    if(date <= DateTime.Now)
                    {
                        context.AddFailure("Invalid Date");
                    }
                });
        }
    }
}
