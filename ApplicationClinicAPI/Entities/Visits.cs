namespace ApplicationClinicAPI.Entities
{
    public class Visits
    {
        public int Id { get; set; }

        public DateTime DateOfVisit { get; set; }

        public Doctors Doctors { get; set; }

        public User User { get; set; }
        
    }
}
