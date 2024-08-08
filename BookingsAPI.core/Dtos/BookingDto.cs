using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingsAPI.core.Dtos
{
    public class BookingDto
    {
        public required string name { get; set; }

        public string? bookingTime { get; set; }
    }
}
