using BusinessLogicLayer.Services.Interface;
using BusinessObject.Entities;
using DataAccessLayer.UoW;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.Implementation
{
    class CategoryService : ICategotiryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ServiceCategory>> GetCategories() => await _unitOfWork.GenericRepository<ServiceCategory>().GetAllAsync();
        
    }
}
