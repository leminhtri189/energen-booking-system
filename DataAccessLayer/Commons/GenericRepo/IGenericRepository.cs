using BusinessObject.Entities;
using DataAccessLayer.Commons;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace DataAccessLayer.Commons.GenericRepo
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> AddAsync(T entity);

        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);

        IEnumerable<T> UpdateRange(IEnumerable<T> entities);

        T? GetById(Guid id);

        Task<T?> GetByIdAsync(Guid id);

        Task<T?> GetByIdAsync(Guid id, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes);

        Task<IEnumerable<T>> GetAllAsync();

        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);

        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? includeProperties = null);

        Task<PaginationResult<T>> AsPaginatedAsync(int page, int page_size, Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? includes = null);

        T? GetFirst(Expression<Func<T, bool>> match);

        Task<T?> GetFirstAsync(Expression<Func<T, bool>> match);

        void Update(T entity);

        void Delete(T entity);

        Task DeleteAsync(Guid id);

        bool Exists(Guid id);

        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);

        int Count();

        Task<int> CountAsync();


    }
}
