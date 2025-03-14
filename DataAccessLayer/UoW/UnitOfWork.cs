using BusinessObject.Entities;
using DataAccessLayer.Commons.GenericRepo;
using DataAccessLayer.Context;
using System.Collections;

namespace DataAccessLayer.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        private Hashtable? _repositories;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IGenericRepository<TEntity> GenericRepository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories == null)
            {
                _repositories = new Hashtable();
            }

            var entityType = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(entityType))
            {
                var repositoryType = typeof(GenericRepository<>);
                var repositoryInstance = Activator.CreateInstance(
                    repositoryType.MakeGenericType(typeof(TEntity)),
                    _context
                );
                _repositories.Add(entityType, repositoryInstance);
            }
            return (IGenericRepository<TEntity>)_repositories[entityType]!;
        }

        public T Repository<T>() where T : class
        {
            if (_repositories == null)
            {
                _repositories = new Hashtable();
            }

            var entityType = typeof(T).Name;

            if (!_repositories.ContainsKey(entityType))
            {
                var repositoryType = typeof(T);
                var repositoryInstance = Activator.CreateInstance(typeof(T),_context);
                _repositories.Add(entityType, repositoryInstance);
            }
            return (T) _repositories[entityType]!;
        }
    }
}
