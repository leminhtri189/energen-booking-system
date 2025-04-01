using Azure;
using BusinessLogicLayer.Services.Interface;
using BusinessObject.Entities;
using DataAccessLayer.Commons;
using DataAccessLayer.Repositories.Implementation;
using DataAccessLayer.Repositories.Interface;
using DataAccessLayer.UoW;
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
        private readonly IServiceRepository _serviceRepository;
        public SkinTimeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _serviceRepository = unitOfWork.Repository<ServiceRepository>();
        }

        public async Task<Service> GetService(Guid id)
        {
            return await _serviceRepository.GetByIdAsync(id);
        }

        public async Task<ICollection<Service>> GetServices(string? searchKey, Guid? categoryId, Guid? skinTypeId, int? page, int? pageSize) => await _serviceRepository.GetServices(searchKey,categoryId,skinTypeId ,page, pageSize);
        public async Task<int> CountTotalServices() => await _unitOfWork.GenericRepository<Service>().CountAsync();

    }
}

