using ApplicationClinicAPI.Entities;
using ApplicationClinicAPI.Exceptions;
using ApplicationClinicAPI.Model.DoctorsModel;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace ApplicationClinicAPI.Services.ServicesDoctors
{
    public class DoctorsServices : IDoctorsServices
    {
        private readonly DatabaseContext _databaseContext;

        //private readonly ILogger _logger;

        private readonly IMapper _mapper;

        private readonly ILogger _logger;
        public DoctorsServices(DatabaseContext databaseContext, IMapper mapper, ILogger<DoctorsServices> logger)
        {
            _databaseContext = databaseContext;
            _logger = logger;
            _mapper = mapper;
        }



        public async Task<List<DoctorsDto>> GetDoctors(int departmentID)
        {
            var doctors = await _databaseContext.Department
                .Include(r => r.ListOfDoctors)
                .ThenInclude(p=>p.ListOfVisits)
                .FirstOrDefaultAsync(Department => Department.Id == departmentID);

            if (doctors is null)
            {
                throw new NotFoundException($"There is no deparments with that id: {departmentID}");
            }
            var result = _mapper.Map<List<DoctorsDto>>(doctors.ListOfDoctors);
            return result;
        }

        public async Task<DoctorsDto> GetDoctorById(int departmentID, int doctorID)
        {
            var department = await _databaseContext.Department
                .Include(r => r.ListOfDoctors)
                .FirstOrDefaultAsync(Department => Department.Id == departmentID);

            if (department is null)
            {
                throw new NotFoundException($"There is no deparments with that id: {departmentID}");
            }
            var currentDoctor = department.ListOfDoctors.FirstOrDefault(r=>r.Id==doctorID);
            if (currentDoctor is null)
            {
                throw new NotFoundException($"There is doctor in {department.Name} with that id: {doctorID}");
            }

            var result = _mapper.Map<DoctorsDto>(currentDoctor);

            return result;

        }


        public async Task<bool> CreateDoctor(CreateDoctorDto createDoctor, int departmentID)
        {
            var departmemt = await _databaseContext.Department.Include(doc=>doc.ListOfDoctors).FirstOrDefaultAsync(dep => dep.Id == departmentID);

            if (departmemt is null)
            {
                throw new NotFoundException($"There is no deparments with that id: {departmentID}");
            }

            departmemt.ListOfDoctors.Add(new Doctors()
            {
                FirstName = createDoctor.FirstName,
                LastName = createDoctor.LastName,
                Title = createDoctor.Title,
                Specialization = createDoctor.Specialization
            });

            await _databaseContext.SaveChangesAsync();
            return true;
        }


        public async Task<bool> UpdateDoctor(UpdateDoctorDto update, int doctorID, int departmentID)
        {
            var department = await _databaseContext.Department
                .Include(r => r.ListOfDoctors)
                .FirstOrDefaultAsync(Department => Department.Id == departmentID);

            if(department is null)
            {
                throw new NotFoundException($"There is no deparments with that id: {departmentID}");
            }

            var currentDoctor = department.ListOfDoctors.FirstOrDefault(r => r.Id == doctorID);

            if (currentDoctor is null)
            {
                throw new NotFoundException($"There is doctor in {department.Name} with that id: {doctorID}");
            }

            if(update.LastName != null) currentDoctor.LastName = update.LastName;
            if(update.Specialization != null) currentDoctor.Specialization = update.Specialization;
            if(update.Title != null) currentDoctor.Title = update.Title ;

            await _databaseContext.SaveChangesAsync();
            return true;
        }
    }
}
