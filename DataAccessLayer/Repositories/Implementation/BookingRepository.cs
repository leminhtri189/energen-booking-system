using BusinessObject.Entities;
using BusinessObject.Enums;
using DataAccessLayer.Commons;
using DataAccessLayer.Commons.GenericRepo;
using DataAccessLayer.Context;
using DataAccessLayer.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Implementation
{
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
    {
        public BookingRepository(ApplicationDbContext context) : base(context) { }

        public async Task<PaginationResult<Booking>> GetCustomerBookingWithStatusPaginated(Guid user_id, int page, int page_size, BookingStatus status, Func<IQueryable<Booking>, IOrderedQueryable<Booking>> order_by)
        {
            IQueryable<Booking> items = context.Set<Booking>()
                .Include(x => x.ServiceNavigation)
                .Include(x => x.TherapistNavigation).ThenInclude(x => x.UserNavigation)
                .Where(x => x.UserId == user_id && x.Status == status);

            if (order_by != null) {
                items = order_by(items);
            }

            return new PaginationResult<Booking>
            {
                TotalPage = (int)Math.Ceiling((double)items.Count() / page_size),
                CurrentPage = page,
                PageSize = page_size,
                TotalItemCount = items.Count(),
                PageContent = items.Skip((page - 1) * page_size).Take(page_size).ToList()
            }; 
        }



        public async Task<IDictionary<TimeOnly, bool>> GetTherapistSchedule(Guid therapistId, string date)
        {
            if (!DateOnly.TryParse(date, out DateOnly selectedDate))
            {
                throw new ArgumentException("Invalid date format", nameof(date));
            }

            selectedDate = selectedDate.AddDays(1);
            var schedules = await ((ApplicationDbContext)context).Bookings
                .Where(b => b.TherapistId == therapistId && b.ReservedDate == selectedDate && b.TransactionNavigation.Status ==PaymentStatus.Sussces)
                .ToListAsync();

            var availability = new Dictionary<TimeOnly, bool>();

            TimeOnly startOfDay = TimeOnly.Parse("08:00:00");
            TimeOnly endOfDay = TimeOnly.Parse("16:30:00");

            for (TimeOnly y = startOfDay; y <= endOfDay; y = y.AddMinutes(30))
            {
                bool isAvailable = !schedules.Any(s =>
     s.ReservedStartTime <= y && y < s.ReservedEndTime);

                availability[y] = isAvailable;
            }

            return availability;
        }



    }
}
