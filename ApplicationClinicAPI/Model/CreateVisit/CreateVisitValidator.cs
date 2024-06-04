using ApplicationClinicAPI.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace ApplicationClinicAPI.Model.CreateVisit
{
    public class CreateVisitValidator:AbstractValidator<Visits>
    {
        public CreateVisitValidator()
        {

            RuleFor(r => r.DateOfVisit)
                .NotEmpty();
               
        }
    }
}
