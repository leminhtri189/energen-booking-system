using BusinessObject.Entities;
using Castle.Components.DictionaryAdapter;
using DataAccessLayer.Commons.GenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Interface
{
    public interface ITherapistRepository :  IGenericRepository<Therapist> 
    {
        Task<ICollection<Therapist>> GetTherapists(string? searchKey, int? pageNumber,int? pageSize );
    }
}
