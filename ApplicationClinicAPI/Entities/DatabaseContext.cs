using Microsoft.EntityFrameworkCore;

namespace ApplicationClinicAPI.Entities
{
    public class DatabaseContext :DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
            //connection-string from configurations
        }
        public DbSet<Department>  Department { get; set; }

        public DbSet<User> Users { get; set; }


        public DbSet<Doctors> Doctors { get; set; }

        public DbSet<Visits> Visits { get; set; }
        public DbSet<Roles> Role { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
        }
    }
}
