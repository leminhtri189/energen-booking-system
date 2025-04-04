using BusinessLogicLayer.Commons;
using BusinessLogicLayer.Services.Interface;
using BusinessObject.Entities;
using BusinessObject.Enums;
using DataAccessLayer.Commons;
using DataAccessLayer.Repositories.Implementation;
using DataAccessLayer.Repositories.Interface;
using DataAccessLayer.UoW;
using Shared.Token;
using System.Security.Cryptography;

namespace BusinessLogicLayer.Services.Implementation
{
    internal class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly ITokenUtilities _tokenUtilities;

        public UserService(IUnitOfWork unit_of_work, ITokenUtilities token_utilities)
        {
            _unitOfWork = unit_of_work;
            _userRepository = unit_of_work.Repository<UserRepository>();
            _tokenUtilities = token_utilities;
        }

        public async Task<ServiceResult> CreateUserAccount(string email, string password, string fullname, string phone, Gender gender, Role role)
        {
            if (_userRepository.GetFirst(x => x.Email == email) != null)
            {
                return ServiceResult.Failed(Error.ValidationFailed("Email has been taken"));
            }

            User user = new User
            {
                Id = Guid.NewGuid(),
                UseName = email,
                Email = email,
                FullName = fullname,
                Phone = phone,
                Password = _tokenUtilities.HashPassword(password),
                Gender = gender,
                Role = role,
                Avatar = "https://product.hstatic.net/1000069970/product/9_2ca4c9ad986041809fbc7707760324d3_large.png",
                CreatedAt = DateTime.Now,
                LastUpdate = DateTime.Now,
                Status = UserStatus.Unverified,
            };

            await _userRepository.AddAsync(user);
            await _unitOfWork.CompleteAsync();

            return ServiceResult.Success();
        }

        public async Task<User?> GetUserWithEmail(string email, string password)
        {
            User? information = await _userRepository.GetFirstAsync(x => x.Email == email);

            Console.WriteLine("Email found");

            if (information == null)
            {
                return null;
            }

            byte[] userHashedPassword = Convert.FromBase64String(information.Password);
            byte[] saltBytes = new byte[16];
            Array.Copy(userHashedPassword, 0, saltBytes, 0, 16);
            Rfc2898DeriveBytes hashingFunction = new Rfc2898DeriveBytes(password, saltBytes, 10000, HashAlgorithmName.SHA256);
            byte[] hashedPasswordBytes = hashingFunction.GetBytes(40);
            byte[] recreatedHash = new byte[saltBytes.Length + hashedPasswordBytes.Length];
            Array.Copy(saltBytes, 0, recreatedHash, 0, 16);
            Array.Copy(hashedPasswordBytes, 0, recreatedHash, 16, hashedPasswordBytes.Length);

            if (Convert.ToBase64String(recreatedHash) != information.Password)
            {
                Console.WriteLine("Password Incorrect");
                return null;
            }

            return information;
        }

        public async Task<ServiceResult> DeleteUser(Guid id)
        {
            User? user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                return ServiceResult.Failed(Error.NotExisted("Can not find user with provided id"));
            }

            user.Status = UserStatus.Removed;
            _userRepository.Update(user);

            await _unitOfWork.CompleteAsync();

            return ServiceResult.Success();
        }

        public async Task<PaginationResult<User>> GetAllUser(int page = 1, int page_size = 10)
        {
            return await _userRepository.AsPaginatedAsync(page, page_size);
        }

        public Task<PaginationResult<User>> GetUserByRole(Role role, int page = 1, int page_size = 10)
        {
            return _userRepository.AsPaginatedAsync(page, page_size, x => x.Role == role);
        }

        public Task<User?> GetUserWithId(Guid id)
        {
            return _userRepository.GetByIdAsync(id);
        }

        public Task<PaginationResult<User>> GetUserWithStatus(UserStatus status, int page = 1, int page_size = 10)
        {
            return _userRepository.AsPaginatedAsync(page, page_size, x => x.Status == status);
        }

        public async Task<ServiceResult> UpdateUserProfile(Guid id, string? username = null, string? email = null, string? fullname = null, string? phone = null, Gender? gender = null, Role? role = null, UserStatus? status = null)
        {
            User? user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                return ServiceResult.Failed(Error.OperationFailed("Can not find user with provided id"));
            }

            user.UseName = username ?? user.UseName;
            user.Email = email ?? user.Email;
            user.FullName = fullname ?? user.FullName;
            user.Phone = phone ?? user.Phone;
            user.Gender = gender ?? user.Gender;
            user.Status = status ?? user.Status;

            _userRepository.Update(user);
            await _unitOfWork.CompleteAsync();

            return ServiceResult.Success(user);
        }
    }
}
