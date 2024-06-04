namespace ApplicationClinicAPI.Model.CreateAccount
{
    public class CreateAcc
    {
        
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; } 

        public string Pesel { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime DateOfRegistration { get; set; } = DateTime.Now;

        public int roleId = 1;
    }
}
