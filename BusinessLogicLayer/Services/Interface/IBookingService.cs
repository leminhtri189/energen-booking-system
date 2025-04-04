using BusinessObject.Entities;
using BusinessObject.Enums;
using DataAccessLayer.Commons;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.Interface
{
    public interface IBookingService
    {
        Task<PaginationResult<Booking>> GetCustomerBookingWithStatus(Guid customer_id, int page, int page_size, BookingStatus status);
        Task<IDictionary<TimeOnly, bool>> GetTherapistSchedule(Guid therapistId, string date);
        Task<string> RequestPayment(Guid userId, Booking booking, string returnAction);
        Task<PaginationResult<Booking>> GetBookingTracking(string user_Id, string role, DateOnly? date = null, int page = 1, int page_size = 10);
        Task CheckIn(Guid bookingId);
        Task CheckOut(Guid bookingId);
        Task Note(Guid bookingId, string note);
    }
}
