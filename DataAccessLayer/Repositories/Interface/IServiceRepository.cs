using BusinessObject.Entities;
using DataAccessLayer.Commons;
using DataAccessLayer.Commons.GenericRepo;
using Microsoft.AspNetCore.Http;

namespace DataAccessLayer.Repositories.Interface
{
    public interface IServiceRepository : IGenericRepository<Service>
    {
        Task<PaginationResult<Service>> GetServices(string? searchKey, Guid? categoryId, Guid? skinTypeId, int? page, int? pageSize);
        Task CreateService(Service service, IFormFile thumbnail, ICollection<IFormFile> serviceImage, List<Guid> SkinTypeIds);
    }
}
