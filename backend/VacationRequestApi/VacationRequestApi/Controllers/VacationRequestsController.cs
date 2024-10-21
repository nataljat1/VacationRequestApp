using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VacationRequestApi.Data.Models;
using VacationRequestApi.Services;
using VacationRequestApi.VacationRequestsDto;

namespace VacationRequestApi.Controllers
{
    [ApiController]
    [Route("api/vacation_requests")]
    [Produces("application/json")]
    public class VacationRequestsController : ControllerBase
    {
        private readonly IVacationRequestService _vacationRequestService;
        private readonly IMapper _mapper;

        public VacationRequestsController(IVacationRequestService vacationRequestService, IMapper mapper)
        {
            _vacationRequestService = vacationRequestService;
            _mapper = mapper;
        }

        // GET: api/VacationRequests
        /// <summary>
        /// Retrieves a list of vacation requests
        /// </summary>
        /// <returns>List of vacation requests</returns>
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<VacationRequestDto>>> GetVacationRequests()
        {
            var requests = await _vacationRequestService.GetVacationRequests();
            var requestDtos = _mapper.Map<IEnumerable<VacationRequestDto>>(requests);
            return Ok(requestDtos);
        }

        // POST: api/VacationRequests
        /// <summary>
        /// Creates a new vacation request
        /// </summary>
        /// <param name="vacationRequestDto">Vacation request details</param>
        /// <returns>Created vacation request</returns>
        [HttpPost("request")]
        public async Task<ActionResult<VacationRequestDto>> PostVacationRequest([FromBody] CreateVacationRequestDto vacationRequestDto)
        {
            // Validate the model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Map DTO to model
            var vacationRequest = _mapper.Map<VacationRequest>(vacationRequestDto);

            // Create the vacation request
            var createdRequest = await _vacationRequestService.CreateVacationRequest(vacationRequest);
            var createdRequestDto = _mapper.Map<VacationRequestDto>(createdRequest);

            return CreatedAtAction(nameof(GetVacationRequests), new { id = createdRequestDto.Id }, createdRequestDto);
        }
    }
}
