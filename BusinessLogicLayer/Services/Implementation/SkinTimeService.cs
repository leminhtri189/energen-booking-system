using Azure;
using BusinessLogicLayer.Services.Interface;
using BusinessObject.Entities;
using DataAccessLayer.Commons;
using DataAccessLayer.Repositories.Implementation;
using DataAccessLayer.Repositories.Interface;
using DataAccessLayer.UoW;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.Implementation
{
    public class SkinTimeService : ISkinTimeService 
    {
       private readonly IUnitOfWork _unitOfWork;
        public SkinTimeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Service GetService(Guid id)
        {
            return  _unitOfWork.Services.GetFirst(s => s.Id == id);
        }
        public async Task<PaginationResult<Service>> GetServices(string? searchKey, Guid? categoryId, Guid? skinTypeId, int? page, int? pageSize) => await _unitOfWork.Services.GetServices(searchKey,categoryId,skinTypeId ,page, pageSize);
        public async Task<int> CountTotalServices() => await _unitOfWork.GenericRepository<Service>().CountAsync();

        public async Task CreateService(Service service, IFormFile thumbnail, ICollection<IFormFile> serviceImage, List<Guid> SkinTypeIds)
      => await _unitOfWork.Services.CreateService(service, thumbnail, serviceImage, SkinTypeIds);
    }
}

