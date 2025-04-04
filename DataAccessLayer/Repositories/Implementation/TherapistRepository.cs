using BusinessObject.Entities;
using BusinessObject.Enums;
using DataAccessLayer.Commons;
using DataAccessLayer.Commons.GenericRepo;
using DataAccessLayer.Context;
using DataAccessLayer.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories.Implementation
{
    public class TherapistRepository : GenericRepository<Therapist>, ITherapistRepository
    {
        public TherapistRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<ICollection<Therapist>> GetTherapists(string? searchKey, int? pageNumber, int? pageSize)
        {
            var query = ((ApplicationDbContext)context).Therapists

         .Include(m => m.UserNavigation)
         .AsQueryable();
            query = query.OrderByDescending(m => m.CreatedAt);
            //if (!string.IsNullOrEmpty(searchKey))
            //{
            //    query = query.Where(m => m.LionName.Contains(searchKey) ||
            //                             m.LionName.Contains(searchKey) ||
            //                             m.Weight.ToString().Contains(searchKey));
            //}

            if (pageNumber.HasValue && pageSize.HasValue && pageNumber > 0 && pageSize > 0)
            {
                query = query
                    .Skip((pageNumber.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<PaginationResult<Therapist>> GetTherapistsPaginated(int page, int page_size, string? searchKey = null)
        {
            return await base.AsPaginatedAsync(page, page_size, 
                x => searchKey != null ? x.Biography!.ToLower().Contains(searchKey.ToLower()) : true,
                x => x.OrderByDescending(x => x.CreatedAt),
                x => x.Include(x => x.UserNavigation));
        }

        public async Task<Therapist?> GetTherapistWIthId(Guid id)
        {
            return await context
                .Set<Therapist>()
                .Include(x => x.UserNavigation)
                .Include(x => x.BookingNavigation)
                .ThenInclude(x => x.FeedbackNavigation)
                .Include(x => x.BookingNavigation)
                .ThenInclude(x => x.CustomerNavigation)
                .FirstOrDefaultAsync(x=> x.Id == id);
        }
    }
}
