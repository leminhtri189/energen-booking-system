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
            booking.ReservedEndTime = booking.ReservedEndTime.AddMinutes(service.Duration);
           await _unitOfWork.GenericRepository<Booking>().AddAsync(booking);
            await _unitOfWork.CompleteAsync();
            returnAction = QueryHelpers.AddQueryString(returnAction, "key", booking.Id.ToString());
            var accessToken = await _payPal.GetAccessToken();
          return await _payPal.CreatePaypalOrderDetail(service.Price, accessToken, returnAction);

        }

        public async Task<PaginationResult<Booking>> GetBookingTracking(string role, string user_Id,  DateOnly? date = null, int page = 1, int page_size = 10)
        {
            Guid userId = Guid.Parse(user_Id);
            DateOnly targetDate = date ?? DateOnly.FromDateTime(DateTime.Today);

            IEnumerable<Booking> items = await _unitOfWork.GenericRepository<Booking>()
     .GetAllAsync(bo => bo.ReservedDate == targetDate);

            var queryableItems = items.AsQueryable();
            if (Enum.TryParse(role, out Role userRole))
            {
                if (userRole == Role.Staff)
                {
                }
                else if (userRole == Role.Therapist)
                {
                    items = items.Where(bo => bo.TherapistNavigation.UserNavigation.Id == userId);
                }
            }

            int totalItemCount = items.Count();
            int totalPage = (int)Math.Ceiling((double)totalItemCount / page_size);
            var pageContent = items.Skip((page - 1) * page_size).Take(page_size).ToList();


            return new PaginationResult<Booking>
            {
                TotalPage = totalPage,
                CurrentPage = page,
                PageSize = page_size,
                TotalItemCount = totalItemCount,
                PageContent = pageContent
            };
        }

        public async Task CheckIn(Guid bookingId)
        {
            var service = await _unitOfWork.GenericRepository<Booking>().GetFirstAsync(se => se.Id == bookingId);
             service.CheckinTime = DateTime.Now;
            service.Status = BookingStatus.InProgress;
            await _unitOfWork.CompleteAsync();
        }
        public async Task Note(Guid bookingId, string note)
        {
            var service = await _unitOfWork.GenericRepository<Booking>().GetFirstAsync(se => se.Id == bookingId);
            service.Note = note;
            await _unitOfWork.CompleteAsync();
        }
        public async Task CheckOut(Guid bookingId)
        {
            var service = await _unitOfWork.GenericRepository<Booking>().GetFirstAsync(se => se.Id == bookingId);
            service.CheckoutTime = DateTime.Now;
            service.Status = BookingStatus.Finished;
            await _unitOfWork.CompleteAsync();
        }
    }
}
