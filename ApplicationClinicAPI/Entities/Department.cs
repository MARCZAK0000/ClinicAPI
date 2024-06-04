namespace ApplicationClinicAPI.Entities
{
    public class Department
    {
        public int Id { get; set; }

        public string Name { get;set; }

        public virtual List<Doctors> ListOfDoctors { get; set; }

    }
}
