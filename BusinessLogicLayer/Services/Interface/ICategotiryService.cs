using BusinessObject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.Interface
{
    public interface ICategotiryService 
    {
        Task<IEnumerable<ServiceCategory>> GetCategories();
    }
}
