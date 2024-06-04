
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;

namespace ApplicationClinicAPI.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime DateOfBirth { get; set; } 

        public DateTime DateOfRegistration { get; set; } 

        public string Pesel { get;set; }

        public int roleID { get; set; }

        public virtual Roles Role { get; set; } 

        public virtual List<Visits> Visits { get; set; }


    }
}
