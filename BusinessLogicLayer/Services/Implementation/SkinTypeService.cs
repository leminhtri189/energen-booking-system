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
    public class SkinTypeService : ISkinTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SkinTypeService(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<SkinType>> GetSkinTypes() => await _unitOfWork.GenericRepository<SkinType>().GetAllAsync();

    }
}
