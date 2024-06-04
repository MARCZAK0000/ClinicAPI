using ApplicationClinicAPI.Entities;
using ApplicationClinicAPI.Exceptions;
using ApplicationClinicAPI.Model.CreateVisit;
using ApplicationClinicAPI.Model.UpdateVisitDate;
using ApplicationClinicAPI.Model.VisitModel;
using ApplicationClinicAPI.Services.ServicesUser;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApplicationClinicAPI.Services.ServicesVisits
{
    public class VisitServices : IVisitServices
    {
        private readonly IMapper _mapper;

        private readonly ILogger _logger;

        private readonly DatabaseContext _databaseContext;

        private readonly IVisitContextServices _visitContextServices;


        public VisitServices(IMapper mapper, ILogger<VisitServices> logger,
            DatabaseContext databaseContext,  IVisitContextServices visitContextServices)
        {
            _mapper = mapper;
            _logger = logger;
            _databaseContext = databaseContext;
            _visitContextServices = visitContextServices;
        }

        public async Task CreateVisit(CreateVisitDto create, int DoctorID)
        {
            if (create == null) throw new NotFoundException("Empty Request");

            var getDoctor = await _databaseContext.Doctors
                .Include(r=>r.ListOfVisits)
                .FirstOrDefaultAsync(r=>r.Id==DoctorID) 
                ?? throw new BadRequestException($"There is no doctor with that ID {DoctorID}");

            var getUser = await _databaseContext.Users
                .Include(r=>r.Visits).FirstOrDefaultAsync(r=>r.Id == _visitContextServices.ID)
                ?? throw new BadRequestException("There is no user with that ID");

            var checkDate = getDoctor.ListOfVisits
                .Where(cond=>cond.DateOfVisit>=DateTime.Now);
            foreach (var item in checkDate)
            {
                if(item.DateOfVisit <= create.DateOfVisit && item.DateOfVisit.AddHours(1) > create.DateOfVisit) 
                {
                    throw new BadRequestException("Doctor has a visit at that date");
                }
            }

            var newVisit = new Visits()
            {
                DateOfVisit = create.DateOfVisit,
                Doctors = getDoctor,
                User = getUser,

            };

            await _databaseContext.AddAsync(newVisit);
            await _databaseContext.SaveChangesAsync();
            
        }

        public async Task UpdateVisitDate(UpdateVisitDto update)
        {
            if (update is null) throw new NotFoundException("Empty Request");

            var getVisit = await _databaseContext.Visits
                .Include(p => p.User)
                .Include(p => p.Doctors)
                .FirstOrDefaultAsync(p => p.Id == update.VisitId) ?? throw new BadRequestException("Server doesn't have informations about this visit");

            var getDoctorVisits = getVisit.Doctors.ListOfVisits
                .Where(r=>r.DateOfVisit > DateTime.Now);

            foreach (var item in getDoctorVisits)
            {
                if (item.DateOfVisit <= update.NewDate && item.DateOfVisit.AddHours(1) > update.NewDate)
                {
                    throw new BadRequestException("Doctor has a visit at that date");
                }
            }

            getVisit.DateOfVisit = update.NewDate;
            await _databaseContext.SaveChangesAsync();

        }


        public async Task<IEnumerable<VisitsDto>> GetVisitsByUser()
        {
            var getVisit = await _databaseContext.Visits
                .Include(p=> p.User)
                .Include(pu => pu.Doctors)
                .Where(rule => rule.User.Id == _visitContextServices.ID)
                .ToArrayAsync()
                ?? throw new NotFoundException("Person not found");
            var result = _mapper.Map<List<VisitsDto>>(getVisit);
            return result;
        }




    }
}
