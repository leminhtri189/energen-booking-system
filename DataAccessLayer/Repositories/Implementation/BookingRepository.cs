using BusinessObject.Entities;
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



        public async Task<IDictionary<TimeOnly, bool>> GetTherapistSchedule(Guid therapistId, string date)
        {
            if (!DateOnly.TryParse(date, out DateOnly selectedDate))
            {
                throw new ArgumentException("Invalid date format", nameof(date));
            }

            DateOnly selectedDateOnly = DateOnly.FromDateTime(selectedDate);

            var schedules = await ((ApplicationDbContext)context).Bookings
                .Where(b => b.TherapistId == therapistId && b.ReservedDate == selectedDate)
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
