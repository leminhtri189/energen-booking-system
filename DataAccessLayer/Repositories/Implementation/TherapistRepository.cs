using BusinessObject.Entities;
using BusinessObject.Enums;
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
    }
}
