using BusinessObject.Entities;
using BusinessObject.Enums;
using DataAccessLayer.Commons;
using DataAccessLayer.Commons.GenericRepo;

namespace DataAccessLayer.Repositories.Interface
{
    public interface IBookingRepository : IGenericRepository<Booking>
    {
        Task<IDictionary<TimeOnly, bool>> GetTherapistSchedule(Guid therapistId, string date);

        Task<PaginationResult<Booking>> GetCustomerBookingWithStatusPaginated(Guid user_id, int page, int page_size, BookingStatus status, Func<IQueryable<Booking>,IOrderedQueryable<Booking>> order_by);
    }
}
