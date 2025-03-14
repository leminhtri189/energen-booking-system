using BusinessObject.Entities;
using DataAccessLayer.Commons;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace DataAccessLayer.Commons.GenericRepo
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        public GenericRepository(DbContext context)
        {
            this.context = context;
        }

        protected DbContext context;

        protected DbSet<T> Set => context.Set<T>();

        protected GenericRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<T> AddAsync(T entity)
        {
            EntityEntry<T> entry = await Set.AddAsync(entity);
            return entry.Entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await Set.AddRangeAsync(entities);
            return entities;
        }

        public int Count()
        {
            return Set.Count();
        }

        public async Task<int> CountAsync()
        {
            return await Set.CountAsync();
        }

        public void Delete(T entity)
        {
            Set.Remove(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            T? target = await Set.FindAsync(id);

            if (target == null)
            {
                throw new InvalidOperationException("entity does not exist!");
            }

            Set.Remove(target);
        }

        public bool Exists(Guid id)
        {
            return Set.Any(x => x.Id == id);
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return await Set.AnyAsync(predicate);
        }

        public T? GetFirst(Expression<Func<T, bool>> match)
        {
            return Set.FirstOrDefault(match);
        }

        public async Task<T?> GetFirstAsync(Expression<Func<T, bool>> match)
        {
            return await Set.FirstOrDefaultAsync(match);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Set.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = Set.AsQueryable();

            foreach (var item in includes)
            {
                query = query.Include(item);
            }

            return await Set.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? includes = null)
        {
            IQueryable<T> query = Set.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includes != null)
            {
                query = includes(query);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }
            return await query.ToListAsync();
        }

        public T? GetById(Guid id)
        {
            return Set.Find(id);
        }

        public virtual async Task<T?> GetByIdAsync(Guid id)
        {
            return await Set.FindAsync(id);
        }

        public virtual void Update(T entity)
        {
            Set.Update(entity);
        }

        public IEnumerable<T> UpdateRange(IEnumerable<T> entities)
        {
            Set.UpdateRange(entities);
            return entities;
        }

        public virtual async Task<PaginationResult<T>> AsPaginatedAsync(int page, int page_size, Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? includes = null)
        {
            if (page <= 0 || page_size <= 0)
            {
                throw new InvalidOperationException("page size and page number must be a positive number");
            }

            IEnumerable<T> data = await GetAllAsync(filter, orderBy, includes);

            return new PaginationResult<T>
            {
                TotalItemCount = data.Count(),
                PageContent = data.Skip((page - 1) * page_size).Take(page_size),
                CurrentPage = page,
                PageSize = page_size,
                TotalPage = data.Count() / page_size,
            };
        }
    }
}
