using BusinessLogicLayer.Commons;
using BusinessLogicLayer.Services.Interface;
using BusinessObject.Entities;
using BusinessObject.Enums;
using DataAccessLayer.Commons;
using DataAccessLayer.Repositories.Implementation;
using DataAccessLayer.Repositories.Interface;
using DataAccessLayer.UoW;

namespace BusinessLogicLayer.Services.Implementation
{
    internal class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserRepository userRepository;

        public UserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            userRepository = unitOfWork.Repository<UserRepository>();
        }

        public Task<ServiceResult> CreateUserAccount(string username, string email, string password, Role user_role)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult> DeleteUser(Guid id)
        {
            User? user = await userRepository.GetByIdAsync(id);

            if (user == null)
            {
                return ServiceResult.Failed(Error.NotExisted("Can not find user with provided id"));
            }

            user.Status = UserStatus.Removed;
            userRepository.Update(user);

            await unitOfWork.CompleteAsync();

            return ServiceResult.Success();
        }

        public async Task<PaginationResult<User>> GetAllUser(int page = 1, int page_size = 10)
        {
            return await userRepository.AsPaginatedAsync(page, page_size);
        }

        public Task<PaginationResult<User>> GetUserByRole(Role role, int page = 1, int page_size = 10)
        {
            return userRepository.AsPaginatedAsync(page, page_size, x => x.Role == role);
        }

        public Task<User?> GetUserWithId(Guid id)
        {
            return userRepository.GetByIdAsync(id);
        }

        public Task<PaginationResult<User>> GetUserWithStatus(UserStatus status, int page = 1, int page_size = 10)
        {
            return userRepository.AsPaginatedAsync(page, page_size, x => x.Status == status);
        }

        public Task<ServiceResult> UpdateUserProfile(Guid id, User user)
        {
            throw new NotImplementedException();
        }
    }
}
