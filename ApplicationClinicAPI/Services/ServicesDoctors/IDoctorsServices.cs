using ApplicationClinicAPI.Model.DoctorsModel;

namespace ApplicationClinicAPI.Services.ServicesDoctors
{
    public interface IDoctorsServices
    {
        Task<DoctorsDto> GetDoctorById(int deparmentID, int doctorID);
        Task<List<DoctorsDto>> GetDoctors(int deparmentID);
        Task<bool> CreateDoctor(CreateDoctorDto createDoctor, int deparmentID);
        Task<bool> UpdateDoctor(UpdateDoctorDto updateDoctor, int doctorID, int deparmentID);
    }
}