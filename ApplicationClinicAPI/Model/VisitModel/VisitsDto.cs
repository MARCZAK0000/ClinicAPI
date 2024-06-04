using ApplicationClinicAPI.Entities;
using Microsoft.Identity.Client;

namespace ApplicationClinicAPI.Model.VisitModel
{
    public class VisitsDto
    {
        public int Id { get; set; }

        public string DoctorName { get; set; }

        public string DoctorSpecializaton { get; set; }
        
        public DateTime DateOfVisit { get; set; }

    }
}
