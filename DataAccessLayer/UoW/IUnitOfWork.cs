using BusinessObject.Entities;
using DataAccessLayer.Commons.GenericRepo;

namespace DataAccessLayer.UoW
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> GenericRepository<TEntity>()
           where TEntity : BaseEntity;

        T Repository<T>() where T : class;

        Task<int> CompleteAsync();
    }
}
