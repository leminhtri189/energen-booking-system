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

        public async Task<PaginationResult<Therapist>> GetTherapistsPaginated(int page, int pageSize)
        {
            var therapistsWithAvgRatingQuery = context.Set<Therapist>()
                .Include(t => t.UserNavigation)
                .Include(t => t.BookingNavigation)
                .Select(t => new
                {
                    Therapist = t,
                    AverageRating = t.BookingNavigation
                        .SelectMany(b => context.Set<Feedback>().Where(f => f.BookingId == b.Id))
                        .Average(f => (double?)f.TherapistRating) ?? 0
                });

            var totalCount = await therapistsWithAvgRatingQuery.CountAsync();

            var paginatedTherapists = await therapistsWithAvgRatingQuery
                .OrderByDescending(x => x.AverageRating) 
                .ThenByDescending(x => x.Therapist.ExperienceYears ?? 0) 
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginationResult<Therapist>
            {
                TotalPage = (int)Math.Ceiling((double)totalCount / pageSize),
                CurrentPage = page,
                PageSize = pageSize,
                TotalItemCount = totalCount,
                PageContent = paginatedTherapists.Select(x => x.Therapist).ToList()
            };
        }

    }
}
