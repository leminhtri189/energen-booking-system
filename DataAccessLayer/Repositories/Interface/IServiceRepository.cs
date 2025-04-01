using BusinessObject.Entities;
using DataAccessLayer.Commons;
using DataAccessLayer.Commons.GenericRepo;

namespace DataAccessLayer.Repositories.Interface
{
    public interface IServiceRepository : IGenericRepository<Service>
    {
        Task<ICollection<Service>> GetServices(string? searchKey, Guid? categoryId, Guid? skinTypeId, int? page, int? pageSize);
    }
}
