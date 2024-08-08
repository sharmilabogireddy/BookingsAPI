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
        private int _maxBookingCapacity = 4;
        private static Dictionary<int,int>  currentBookings = new Dictionary<int,int>();
        private static TimeSpan bookingStartTime = new TimeSpan(9, 0, 0);
        private static TimeSpan bookingEndTime = new TimeSpan(16, 0, 0);
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

            /*
            //Check for data validations
            if (string.IsNullOrEmpty(requestModel.name))
            {
                return BadRequest("Name is not valid.");
            }
            if (string.IsNullOrEmpty(requestModel.bookingTime))
            {
                return BadRequest("Booking time is not valid.");
            }

            if(!TimeSpan.TryParse(requestModel.bookingTime, out TimeSpan bookingTimeObject))
            {
                return BadRequest("Booking time is not valid.");
            }

            bool isValidBookingTime = bookingTimeObject >= bookingStartTime && bookingTimeObject <= bookingEndTime;

            if (!isValidBookingTime)
            {
                return BadRequest("Booking time is not in business hours.");
            }

            int defaultCount = 0;
            if (!currentBookings.TryGetValue(bookingTimeObject.Hours, out int count))
            {
                count = defaultCount;
            }

            //Check for max simultaneous bookings.
            if(count < _maxBookingCapacity)
            {
                count++;
                currentBookings[bookingTimeObject.Hours] = count;
            }
            else
            {
                return Conflict("Bookings are full at the specified time.");
            }*/

            BookingResponse response = new BookingResponse()
            {
                bookingId = Guid.NewGuid()
            };
            return Ok(response);
        }
    }
}
