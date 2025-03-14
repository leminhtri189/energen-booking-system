using BusinessObject.Entities;
using DataAccessLayer.Commons.GenericRepo;
using DataAccessLayer.Context;
using DataAccessLayer.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Implementation
{
    internal class ServiceRepository : GenericRepository<Service>, IServiceRepository
    {
        public ServiceRepository(ApplicationDbContext context) : base(context) { }
    }
}
