using BookingsAPI.core.Requests;
using BookingsAPI.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create a new booking
        /// </summary>
        /// <param name="requestModel">The data to create booking.</param>
        /// <returns>The bookingId</returns>
        [HttpPost()]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BookingResponse>> CreateBooking([FromBody] BookingRequest requestModel)
        {
            var result = await _mediator.Send(new CreateBookingRequest()
            {
                name = requestModel.name,
                bookingTime = requestModel.bookingTime
            });

            BookingResponse response = new BookingResponse()
            {
                bookingId = Guid.NewGuid()
            };
            return Ok(response);
        }
    }
}
