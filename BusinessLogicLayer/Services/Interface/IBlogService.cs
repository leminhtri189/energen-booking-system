using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Entities;
using DataAccessLayer.Commons;

namespace BusinessLogicLayer.Services.Interface
{
    public interface IBlogService
    {
        Task<PaginationResult<Blog>> GetBlogPaginated(int page, int page_size);

        Task<Blog?> GetBlogWithId(Guid id);
    }
}
