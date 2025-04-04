using BusinessObject.Entities;
using DataAccessLayer.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.Interface
{
    public interface ITherapistService
    {
        Task<Therapist> GetTherapistWithIdAsync(Guid id);

        Task<ICollection<Therapist>> GetTherapists(string? searchKey, int? pageNumber, int? pageSize);

        Task<PaginationResult<Therapist>> GetTherapistPaginatedAsync(string? searchKey, int page, int page_size);
    }
}
