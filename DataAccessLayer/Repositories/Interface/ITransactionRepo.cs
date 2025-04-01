using BusinessObject.Entities;
using DataAccessLayer.Commons.GenericRepo;

namespace DataAccessLayer.Repositories.Interface
{
    public interface ITransactionRepo : IGenericRepository<Transaction>
    {
        Task CreateAsync(Booking booking, bool isSuscess);
    }
}
