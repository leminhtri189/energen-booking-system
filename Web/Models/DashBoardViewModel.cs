using BusinessObject.Entities;
using DataAccessLayer.Commons;

namespace Web.Models
{
    public class DashBoardViewModel
    {
        public PaginationResult<Service> Services { get; set; }
        public PaginationResult<User> Users { get; set; }
        public PaginationResult<Booking> Bookings { get; set; }
        public PaginationResult<Transaction> Transactions { get; set; }
        public BookingChartViewModel BookingChart { get; set; }
    }
    public class BookingChartViewModel
    {
        public string[] Labels { get; set; }
        public int[] BookingsData { get; set; }
    }
}
