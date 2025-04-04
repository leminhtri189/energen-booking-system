using BusinessLogicLayer.Services.Interface;
using BusinessObject.Entities;
using DataAccessLayer.Commons;
using BusinessObject.Enums;
using DataAccessLayer.Repositories.Interface;
using DataAccessLayer.UoW;
using Google.Apis.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.Implementation
{
    public class TherapistService : ITherapistService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TherapistService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginationResult<Therapist>> GetTherapistPaginatedAsync( int page, int page_size)
        {
            return await _unitOfWork.Therapists.GetTherapistsPaginated(page, page_size);
        }

        public Task<PaginationResult<Therapist>> GetTherapistPaginatedAsync(string? searchKey, int page, int page_size)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Therapist>> GetTherapists(string? searchKey, int? pageNumber, int? pageSize) => await
            _unitOfWork.Therapists.GetTherapists(searchKey, pageNumber, pageSize);

        public async Task<int> GetTherapistsCount()
        {
            var listTherapist = await _unitOfWork.GenericRepository<User>().GetAllAsync(us => us.Role == Role.Therapist);
            return listTherapist.Count();

        }
        public async Task<PaginationResult<Therapist>> GetTherapistsToBooking(int page, int page_size)
        {
            return await _unitOfWork.Therapists.GetTherapistsPaginated(page, page_size);
            

        }

        public async Task<Therapist> GetTherapistWithIdAsync(Guid id)
        {
            return await _unitOfWork.Therapists.GetTherapistWIthId(id);
        }
    }
}