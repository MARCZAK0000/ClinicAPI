using ApplicationClinicAPI.Model.CreateVisit;
using ApplicationClinicAPI.Model.UpdateVisitDate;
using ApplicationClinicAPI.Services.ServicesVisits;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationClinicAPI.Controllers
{
    [Route("api/visit")]
    [ApiController]
    [Authorize]
    public class VisitController : ControllerBase
    {
        private readonly IVisitServices _visitServices;

        public VisitController(IVisitServices visitServices)
        {
            _visitServices = visitServices;
        }


        [HttpPost("{DoctorID}")]
        public async Task<ActionResult> CreateVisit([FromBody] CreateVisitDto createVisit, [FromRoute] int DoctorID)
        {
            await _visitServices.CreateVisit(createVisit, DoctorID);
            var createdResource = new { Good = 1, Version = "1.0" };
            return Created(string.Empty, createdResource);
        }


        [HttpGet("all_user")]
        public async Task<ActionResult> GetAllUserVisit()
        {
            var result = await _visitServices.GetVisitsByUser();


            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<ActionResult> UpdateVisit([FromBody] UpdateVisitDto updateVisit)
        {

            await _visitServices.UpdateVisitDate(updateVisit);
            return Ok();
        }

    }
}
