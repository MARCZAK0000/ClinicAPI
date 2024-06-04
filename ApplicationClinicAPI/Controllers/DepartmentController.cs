using ApplicationClinicAPI.Services.ServicesDepartment;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationClinicAPI.Controllers
{
    [Route("api/departments")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentServices _departmentServices;

        public DepartmentController(IDepartmentServices departmentServices)
        {
            _departmentServices = departmentServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _departmentServices.GetAll();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var result = await _departmentServices.GetById(id);

            return Ok(result);
        }
        [HttpGet("names")]
        public async Task<IActionResult> GetNames()
        {
            var result = await _departmentServices.GetOnlyDepatments();

            return Ok(result);
        }
    }
}
