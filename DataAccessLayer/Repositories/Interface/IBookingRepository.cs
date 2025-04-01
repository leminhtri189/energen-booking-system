using BusinessObject.Entities;
using DataAccessLayer.Commons.GenericRepo;

namespace DataAccessLayer.Repositories.Interface
{
    public interface IBookingRepository : IGenericRepository<Booking>
    {
        Task<IDictionary<TimeOnly, bool>> GetTherapistSchedule(Guid therapistId, string date);
    }
}
