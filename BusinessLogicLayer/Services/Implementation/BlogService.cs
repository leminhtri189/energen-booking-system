using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Services.Interface;
using BusinessObject.Entities;
using DataAccessLayer.Commons;
using DataAccessLayer.Repositories.Interface;
using DataAccessLayer.UoW;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogicLayer.Services.Implementation
{
    public class BlogService: IBlogService
    {
        private readonly IUnitOfWork _uow;
        private readonly IBlogRepository _repo;

        public BlogService(IUnitOfWork uow, IBlogRepository repo)
        {
            _uow = uow;
            _repo = repo;
        }

        public Task<PaginationResult<Blog>> GetBlogPaginated(int page, int page_size)
        {
            return _repo.AsPaginatedAsync(page, page_size, 
                orderBy: x => x.OrderByDescending(x => x.CreatedAt), 
                includes: x => x.Include(x => x.UserNavigation));
        }

        public Task<Blog?> GetBlogWithId(Guid id)
        {
            return _repo.GetByIdAsync(id, x => x.Include(x => x.UserNavigation));
        }
    }
}
