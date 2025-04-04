using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Entities;
using DataAccessLayer.Commons.GenericRepo;
using DataAccessLayer.Context;
using DataAccessLayer.Repositories.Interface;

namespace DataAccessLayer.Repositories.Implementation
{
    public class BlogRepository: GenericRepository<Blog>, IBlogRepository
    {
        public BlogRepository(ApplicationDbContext context) : base(context) { }
    }
}
