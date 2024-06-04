namespace ApplicationClinicAPI.Entities
{
    public abstract class DatabaseData
    {
        protected IEnumerable<User> GetUser()
        {
            var users = new List<User>()
            {
                new User()
                {

                }
            };

            return users;
        }


        protected IEnumerable<Roles> GetRoles()
        {
            var roles = new List<Roles>()
            {
               new Roles()
               {
                   Name = "User",
               },
                new Roles()
               {
                   Name = "Manager"
               },
               new Roles()
               {
                   Name = "admin",
               }
            };



            return roles;
        }

        protected IEnumerable<Department> GetDepartments()
        {
            var department = new List<Department>()
           {
               new Department()
               {
                   Name = "Ginekologia",
                   ListOfDoctors = new List<Doctors>()
                   {
                       new Doctors()
                       {
                           FirstName = "Leon",
                           LastName = "Kowalski",
                           Title = "Dr.hab",
                           Specialization = "Ginekologia"
                       }, new Doctors()
                       {
                           FirstName = "Martyna",
                           LastName = "Kaszuba",
                           Title = "Dr",
                           Specialization = "Ginekologia"
                       }
                   }
               },
               new Department()
               {
                   Name = "Stomatologia",
                   ListOfDoctors = new List<Doctors>()
                   {
                       new Doctors()
                       {
                           FirstName = "Michał",
                           LastName = "Kozera",
                           Title = "Dr",
                           Specialization = "Stomatologia"
                       }, new Doctors()
                       {
                           FirstName = "Monika",
                           LastName = "Jaworek",
                           Title = "Dr",
                           Specialization = "Stomatologia"
                       }
                   }
               } ,
               new Department()
               {
                   Name = "Onkologia",
                   ListOfDoctors = new List<Doctors>()
                   {
                       new Doctors()
                       {
                           FirstName = "Polikarp",
                           LastName = "Miszt",
                           Title = "Dr",
                           Specialization = "Onkologia"
                       }, new Doctors()
                       {
                           FirstName = "Marta",
                           LastName = "Kmicic",
                           Title = "Dr",
                           Specialization = "Onkologia"
                       }
                   }
               },
               new Department()
               {
                   Name = "Pediatria",
                   ListOfDoctors = new List<Doctors>()
                   {
                       new Doctors()
                       {
                           FirstName = "Przemysłwa",
                           LastName = "Mruk",
                           Title = "Dr",
                           Specialization = "Pediatria"
                       }, new Doctors()
                       {
                           FirstName = "Marlena",
                           LastName = "Serwacka",
                           Title = "Dr",
                           Specialization = "Pediatria"
                       }
                   }
               }
           };

            return department;
        }
    }
}
