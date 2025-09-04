using Booking.Application.Abstract;
using Booking.Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;


namespace Booking.Api.Controllers
{
    [Route("api/available-homes")]
    [ApiController]
    public class AvailableHomesController : ControllerBase
    {
        private readonly IAvailabilityService _service;

        public AvailableHomesController(IAvailabilityService service) => _service = service;

        [HttpGet]
        [ProducesResponseType(typeof(AvailableHomesResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get([FromQuery] string startDate, [FromQuery] string endDate, CancellationToken ct)
        {
            if (!TryParseIsoDate(startDate, out var start) || !TryParseIsoDate(endDate, out var end))
                return BadRequest("Dates must be in YYYY-MM-DD format.");


            try
            {
                var homes = await _service.FindAvailableHomesAsync(start, end, ct);
                return Ok(new AvailableHomesResponse("OK", homes));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        private static bool TryParseIsoDate(string input, out DateOnly date)
              => DateOnly.TryParseExact(input, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);
    }
}

