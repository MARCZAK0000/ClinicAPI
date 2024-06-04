using ApplicationClinicAPI.Entities;
using ApplicationClinicAPI.Exceptions;
using ApplicationClinicAPI.Model.DepartmentModel;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApplicationClinicAPI.Services.ServicesDepartment
{

    public class DepartmentServices : IDepartmentServices
    {
        private readonly DatabaseContext _databaseContext;

        private readonly ILogger _logger;

        private readonly IMapper _mapper;

        public DepartmentServices(DatabaseContext databaseContext, ILogger<DepartmentServices> logger, IMapper mapper )
        {
            _databaseContext = databaseContext;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Department>> GetAll()
        {

            var departments = await _databaseContext.Department
                .Include(inc => inc.ListOfDoctors).ToArrayAsync();
            if (departments is null)
            {
                throw new NotFoundException("There is no departments");
            }
            _logger.LogInformation("Get All");
            return departments;
        }



        public async Task<Department> GetById(int id)
        {
            var departmens = await _databaseContext.Department
                .Include(inc => inc.ListOfDoctors)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (departmens is null)
            {
                throw new NotFoundException($"We cannot found department with that id: {id}");
            }

            return departmens;
        }

        public async Task<IEnumerable<DepartmentDto>> GetOnlyDepatments()
        {
            var departments = await _databaseContext.Department.ToArrayAsync();

            if (departments is null) throw new NotFoundException("Not found any departments"); 
            var result = _mapper.Map<List<DepartmentDto>>(departments);
            return result;
            
        }
    }
}
