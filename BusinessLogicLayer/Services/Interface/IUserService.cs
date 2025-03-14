using BusinessLogicLayer.Commons;
using BusinessObject.Entities;
using BusinessObject.Enums;
using DataAccessLayer.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.Interface
{
    public interface IUserService
    {
        public Task<User?> GetUserWithId(Guid id);

        public Task<PaginationResult<User>> GetAllUser(int page = 1, int page_size = 10);

        public Task<PaginationResult<User>> GetUserWithStatus(UserStatus status, int page = 1, int page_size = 10);

        public Task<PaginationResult<User>> GetUserByRole(Role role, int page = 1, int page_size = 10);

        public Task<ServiceResult> CreateUserAccount(string username, string email, string password, Role user_role);

        public Task<ServiceResult> UpdateUserProfile(Guid id, User user);

        public Task<ServiceResult> DeleteUser(Guid id);
    }
}
