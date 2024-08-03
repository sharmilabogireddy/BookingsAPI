using BookingsAPI.Controllers;
using BookingsAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Xunit;

namespace BookingApi.Tests
{
    public class BookingsApiTest
    {
        private readonly BookingController _contoller;
        private int _maxBookingCapacity = 4;

        public BookingsApiTest()
        {

            _contoller = new BookingController();
        }

        [Fact]
        public void CreateBooking_OKResult()
        {
            var requestModel = new BookingRequest(){ name = "John",bookingTime = "10:00" };

            var response =  _contoller.CreateBooking(requestModel);

            var okResult = Assert.IsType<OkObjectResult>(response.Result);
            Assert.Equal(200, okResult.StatusCode);
            BookingResponse bookingResponse = (BookingResponse)okResult.Value;
            Assert.NotNull(bookingResponse.bookingId);
        }

        [Fact]
        public void CreateBooking_NameNotValid()
        {
            var requestModel = new BookingRequest() { name = null, bookingTime = "09:00" };

            var response = _contoller.CreateBooking(requestModel);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(response.Result);
            Assert.Equal(400, badRequestResult.StatusCode);
            Assert.Equal("Name is not valid.", badRequestResult.Value);
        }

        [Fact]
        public void CreateBooking_BookingTimeNotValid()
        {
            var requestModel = new BookingRequest() { name = "John", bookingTime = "" };

            var response = _contoller.CreateBooking(requestModel);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(response.Result);
            Assert.Equal(400, badRequestResult.StatusCode);
            Assert.Equal("Booking time is not valid.", badRequestResult.Value);
        }

        [Fact]
        public void CreateBooking_BookingTimeOutOfOfficeHours()
        {
            var requestModel = new BookingRequest() { name = "John", bookingTime = "08:00" };

            var response = _contoller.CreateBooking(requestModel);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(response.Result);
            Assert.Equal(400, badRequestResult.StatusCode);
            Assert.Equal("Booking time is not in business hours.", badRequestResult.Value);
        }

        [Fact]
        public void CreateBooking_ConflictResult()
        {
            var requestModel = new BookingRequest() { name = "John", bookingTime = "09:00" };
            for(int i=1; i<=_maxBookingCapacity+1; i++)
            {
                var response = _contoller.CreateBooking(requestModel);

                if (i == 5)
                {
                    var conflictResult = Assert.IsType<ConflictObjectResult>(response.Result);
                    Assert.Equal(409, conflictResult.StatusCode);
                    Assert.Equal("Bookings are full at the specified time.", conflictResult.Value);
                }
                else
                {
                    var okResult = Assert.IsType<OkObjectResult>(response.Result);
                    Assert.Equal(200, okResult.StatusCode);
                    BookingResponse bookingResponse = (BookingResponse)okResult.Value;
                    Assert.NotNull(bookingResponse.bookingId);
                }
            }
          
        }
    }
}