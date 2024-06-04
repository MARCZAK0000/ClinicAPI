using ApplicationClinicAPI.Entities;
using ApplicationClinicAPI.Model.VisitModel;

namespace ApplicationClinicAPI.Model.UserAccountDto
{
    public class UserAccDto
    {
    
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime DateOfBirth { get; set; }


        public string Pesel { get; set; }

        public virtual List<VisitsDto> Visits { get; set; }
    }
}
