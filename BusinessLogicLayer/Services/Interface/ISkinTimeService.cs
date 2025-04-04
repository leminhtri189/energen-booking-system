using BusinessObject.Entities;
using DataAccessLayer.Commons;
using DataAccessLayer.Commons.GenericRepo;
using DataAccessLayer.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.Interface
{
    public interface ISkinTimeService
    {
        Task<PaginationResult<Service>> GetServices(string? searchKey, Guid? categoryId, Guid? skinTypeId, int? page,int? pageSize);
        Service GetService(Guid id);
        Task CreateService(Service service, IFormFile thumbnail, ICollection<IFormFile> serviceImage, List<Guid> SkinTypeIds);
        Task<int> CountTotalServices();
    }
}
