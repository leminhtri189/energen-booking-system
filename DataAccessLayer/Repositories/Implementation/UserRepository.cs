using BusinessObject.Entities;
using BusinessObject.Enums;
using DataAccessLayer.Commons;
using DataAccessLayer.Commons.GenericRepo;
using DataAccessLayer.Context;
using DataAccessLayer.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories.Implementation
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<PaginationResult<User>> GetAllStaffAsync(int page, int page_size, bool include_removed = false)
        {
            return await AsPaginatedAsync(page, page_size, filter: x => x.Role == Role.Staff);
        }

        public async Task<PaginationResult<User>> GetAllTherapistAsync(int page, int page_size, bool include_removed = false)
        {
            return await AsPaginatedAsync(page, page_size, filter: x => x.Role == Role.Therapist, includes: x => x.Include(x => x.Therapists));
        }
    }
}
