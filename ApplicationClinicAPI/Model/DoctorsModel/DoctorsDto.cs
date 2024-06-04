using ApplicationClinicAPI.Entities;

namespace ApplicationClinicAPI.Model.DoctorsModel
{
    public class DoctorsDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Title { get; set; }

        public string Specialization { get; set; }

        public virtual List<Visits> ListVisits { get; set; }
    }
}
