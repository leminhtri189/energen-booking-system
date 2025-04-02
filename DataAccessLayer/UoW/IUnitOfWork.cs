using BusinessObject.Entities;
using DataAccessLayer.Commons.GenericRepo;
using DataAccessLayer.Repositories.Interface;

namespace DataAccessLayer.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        IBookingRepository Bookings { get; }
        IQuestionRepository Questions { get; }
        IUserRepository Users { get; }
        IServiceRepository Services { get; }
        ITransactionRepo Transactions{ get; }
        IGenericRepository<TEntity> GenericRepository<TEntity>()
           where TEntity : BaseEntity;

        T Repository<T>() where T : class;

        Task<int> CompleteAsync();
    }
}
