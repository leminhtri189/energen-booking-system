using BusinessObject.Entities;
using DataAccessLayer.Commons;
using DataAccessLayer.Commons.GenericRepo;

namespace DataAccessLayer.Repositories.Interface
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<PaginationResult<User>> GetAllTherapistAsync(int page, int page_size, bool include_removed = false);

        Task<PaginationResult<User>> GetAllStaffAsync(int page, int page_size, bool include_removed = false);
    }
}
