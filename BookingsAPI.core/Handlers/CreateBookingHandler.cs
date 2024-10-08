﻿using BookingsAPI.core.Requests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingsAPI.core.Dtos;
using AutoMapper;
using BookingsAPI.shared.Common;
using System.ComponentModel.DataAnnotations;
using BookingsAPI.core.Entities;


namespace BookingsAPI.core.Handlers
{
    public class CreateBookingHandler : IRequestHandler<CreateBookingRequest, BookingDto>
    {
        private int _maxBookingCapacity = 4;
        private static Dictionary<int, int> currentBookings = new Dictionary<int, int>();
        private static TimeSpan bookingStartTime = new TimeSpan(9, 0, 0);
        private static TimeSpan bookingEndTime = new TimeSpan(16, 0, 0);

        public async Task<BookingDto> Handle(CreateBookingRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.name))
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(ConstantStrings.NAME_IVALID);
            }
            if (string.IsNullOrEmpty(request.bookingTime))
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(ConstantStrings.BOOKING_TIME_IVALID);
            }

            if (!TimeSpan.TryParse(request.bookingTime, out TimeSpan bookingTimeObject))
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(ConstantStrings.BOOKING_TIME_IVALID);

            }

            bool isValidBookingTime = bookingTimeObject >= bookingStartTime && bookingTimeObject <= bookingEndTime;

            if (!isValidBookingTime)
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(ConstantStrings.BOOKING_TIME_NOT_IN_BUSSINESS_HOURS);
            }

            int defaultCount = 0;
            if (!currentBookings.TryGetValue(bookingTimeObject.Hours, out int count))
            {
                count = defaultCount;
            }

            //Check for max simultaneous bookings.
            if (count < _maxBookingCapacity)
            {
                count++;
                currentBookings[bookingTimeObject.Hours] = count;
            }
            else
            {
                throw new System.ComponentModel.DataAnnotations.ValidationException(ConstantStrings.BOOKING_TIME_IVALID);
            }
            BookingDto booking = new BookingDto() { name = request.name, bookingTime = request.bookingTime };

            return booking;
        }

        
    }
}
