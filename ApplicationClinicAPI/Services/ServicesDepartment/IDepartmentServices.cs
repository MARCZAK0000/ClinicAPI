using ApplicationClinicAPI.Entities;
using ApplicationClinicAPI.Model.DepartmentModel;

namespace ApplicationClinicAPI.Services.ServicesDepartment
{
    public interface IDepartmentServices
    {
        Task<IEnumerable<Department>> GetAll();
        Task<Department> GetById(int id);
        Task<IEnumerable<DepartmentDto>> GetOnlyDepatments();
    }
}