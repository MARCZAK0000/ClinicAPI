using ApplicationClinicAPI.Entities;
using ApplicationClinicAPI.Model.DepartmentModel;
using ApplicationClinicAPI.Model.DoctorsModel;
using ApplicationClinicAPI.Model.VisitModel;
using AutoMapper;

namespace ApplicationClinicAPI.Model.MappingModel
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Doctors, DoctorsDto>();
            CreateMap<Department, DepartmentDto>();

            CreateMap<Visits, VisitsDto>()
                .ForMember(req => req.DoctorName, res => res.MapFrom(from => from.Doctors.FirstName + " " + from.Doctors.LastName))
                .ForMember(req => req.DoctorSpecializaton, res => res.MapFrom(from => from.Doctors.Specialization));
           
        }
    }
}
