using BusinessObject.Entities;
using DataAccessLayer.Commons.GenericRepo;
using DataAccessLayer.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.Interface
{
    public interface ISkinTimeService
    {
        Task<ICollection<Service>> GetServices(string? searchKey, Guid? categoryId, Guid? skinTypeId, int? page,int? pageSize);
        Task<Service> GetService(Guid id);
        Task<int> CountTotalServices();
    }
}
