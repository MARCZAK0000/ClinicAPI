

using ApplicationClinicAPI.Entities;

namespace ApplicationClinicAPI.SeederDatabase
{
    public class SeederDb : DatabaseData
    {

        private readonly DatabaseContext _dbContext;

        public SeederDb(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if( _dbContext.Database.CanConnect()) 
            {
                if (!_dbContext.Role.Any())
                {
                    var data = GetRoles();
                    _dbContext.Role.AddRange(data);
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.Department.Any())
                {
                    var data = GetDepartments();
                    _dbContext.Department.AddRange(data);
                    _dbContext.SaveChanges();
                }
            }

            
        }

       
    }
}
