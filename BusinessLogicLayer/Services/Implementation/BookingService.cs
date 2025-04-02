using BusinessLogicLayer.Commons;
using BusinessLogicLayer.Services.Interface;
using BusinessObject.Entities;
using BusinessObject.Enums;
using DataAccessLayer.Commons;
using DataAccessLayer.UoW;
using Google.Api.Gax;
using Microsoft.AspNetCore.WebUtilities;
using Shared.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.Implementation
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PayPal _payPal;
        public BookingService(IUnitOfWork unitOfWork,PayPal payPal)
        {
            _payPal = payPal;
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginationResult<Booking>> GetCustomerBookingWithStatus(Guid customer_id, int page, int page_size, BookingStatus status)
        {
            return await _unitOfWork.Bookings.GetCustomerBookingWithStatusPaginated(
                customer_id, page, page_size, status,
                booking => booking.OrderByDescending(x => x.ReservedDate).ThenByDescending(x => x.ReservedStartTime));
;
        }

        public async Task<IDictionary<TimeOnly, bool>> GetTherapistSchedule(Guid therapistId, string date)=> await _unitOfWork.Bookings.GetTherapistSchedule(therapistId,date);

        public async Task<string> RequestPayment(Guid userId, Booking booking, string returnAction)
        {
            var service = await _unitOfWork.GenericRepository<Service>().GetFirstAsync(se => se.Id == booking.ServiceId);
           booking.Id = Guid.NewGuid();
            booking.UserId = userId;
           await _unitOfWork.GenericRepository<Booking>().AddAsync(booking);
            await _unitOfWork.CompleteAsync();
            returnAction = QueryHelpers.AddQueryString(returnAction, "key", booking.Id.ToString());
            var accessToken = await _payPal.GetAccessToken();
          return await _payPal.CreatePaypalOrderDetail(service.Price, accessToken, returnAction);

        }

    }
}
