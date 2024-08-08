using System.ComponentModel.DataAnnotations;

namespace BookingsAPI.Models
{
    public class BookingRequest
    {
        //[Required(ErrorMessage = "Name is required.")]
        public string name { get; set; }

        public string? bookingTime { get; set; }
    }
}
