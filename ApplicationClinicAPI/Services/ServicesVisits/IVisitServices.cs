using ApplicationClinicAPI.Model.CreateVisit;
using ApplicationClinicAPI.Model.UpdateVisitDate;
using ApplicationClinicAPI.Model.VisitModel;

namespace ApplicationClinicAPI.Services.ServicesVisits
{
    public interface IVisitServices
    {
        Task CreateVisit(CreateVisitDto create, int DoctorID);

        Task UpdateVisitDate(UpdateVisitDto update);

        Task<IEnumerable<VisitsDto>> GetVisitsByUser();
    }
}
