using ApplicationClinicAPI.Model.DoctorsModel;
using ApplicationClinicAPI.Services.ServicesDoctors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationClinicAPI.Controllers
{
    [Route("api/departments/{id}/doctors")]
    [ApiController]
    [Authorize]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorsServices _doctorServices;

        public DoctorsController(IDoctorsServices doctorServices)
        {
            _doctorServices = doctorServices;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllDoctorsFromDepartment([FromRoute]int id)
        {
            var result = await _doctorServices.GetDoctors(id);
            return Ok(result);  
        }

        [HttpGet("{doctorID}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDoctorsByID([FromRoute] int id, [FromRoute] int doctorID)
        {
            var result = await _doctorServices.GetDoctorById(id, doctorID);

            return Ok(result);  
        }
        [HttpPost()]
        [Authorize(Roles = "Manager")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateDoctor([FromBody] CreateDoctorDto doctorsDto, [FromRoute] int id)
        {
            var result = await _doctorServices.CreateDoctor(doctorsDto, id);

            return Created($"Created with departamentn id: {id}", result);
        }


        [HttpPut("{doctorID}")]
        [Authorize(Roles = "Manager")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateDoctor([FromBody] UpdateDoctorDto update, [FromRoute] int id, [FromRoute] int doctorID)
        {
            var result = await _doctorServices.UpdateDoctor(update, doctorID, id);

            return Created($"Created with departamentn id: {id}", result);
        } 



    }
}
