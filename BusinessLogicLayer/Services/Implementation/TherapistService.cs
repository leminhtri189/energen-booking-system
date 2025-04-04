using BusinessLogicLayer.Services.Interface;
using BusinessObject.Entities;
using DataAccessLayer.Commons;
using DataAccessLayer.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.Implementation
{
    public class TherapistService : ITherapistService
    {
        private readonly ITherapistRepository _repository;
        public TherapistService(ITherapistRepository therapistRepository)
        {
            _repository = therapistRepository;
        }

        public async Task<PaginationResult<Therapist>> GetTherapistPaginatedAsync(string? searchKey, int page, int page_size)
        {
            return await _repository.GetTherapistsPaginated(page, page_size, searchKey);
        }

        public async Task<ICollection<Therapist>> GetTherapists(string? searchKey, int? pageNumber, int? pageSize) => await
            _repository.GetTherapists(searchKey, pageNumber, pageSize);

        public async Task<Therapist> GetTherapistWithIdAsync(Guid id)
        {
            return await _repository.GetTherapistWIthId(id);
        }
    }
}
