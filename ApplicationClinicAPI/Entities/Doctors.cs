namespace ApplicationClinicAPI.Entities
{
    public class Doctors
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Title { get; set; }

        public string Specialization { get; set; }

        public virtual List<Visits> ListOfVisits { get; set; }
    }
}
