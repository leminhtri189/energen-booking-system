using BusinessObject.Entities;
using BusinessObject.Enums;
using DataAccessLayer.Commons;
using DataAccessLayer.Commons.GenericRepo;
using DataAccessLayer.Context;
using DataAccessLayer.Repositories.Interface;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.Implementation
{
    public class ServiceRepository : GenericRepository<Service>, IServiceRepository
    {
        public ServiceRepository(ApplicationDbContext context) : base(context) { }

        public async Task<PaginationResult<Service>> GetService(int page, int page_size, bool include_removed = false)
        {
            return await AsPaginatedAsync(page, page_size, filter: x => x.Status == ServiceStatus.Available);
        }

        public async Task<ICollection<Service>> GetServices(string? searchKey, Guid? categoryId, Guid? skinTypeId, int? page, int? pageSize)
        {
            var query = ((ApplicationDbContext)context).Services
                .Include(m => m.ServiceCategoryNavigation) // Load thông tin danh mục
                .Include(m => m.SkinTypes) // Load trực tiếp danh sách SkinType
                .OrderByDescending(m => m.CreatedAt)
                .AsQueryable();

            // Lọc theo từ khóa tìm kiếm
            if (!string.IsNullOrEmpty(searchKey))
            {
                query = query.Where(m => m.ServiceName.Contains(searchKey) ||
                                         m.Description.Contains(searchKey));
            }

            // Lọc theo danh mục
            if (categoryId.HasValue)
            {
                query = query.Where(m => m.ServiceCategoryId == categoryId);
            }

            // Lọc theo loại da (Many-to-Many trực tiếp, không có bảng trung gian)
            if (skinTypeId.HasValue)
            {
                query = query.Where(m => m.SkinTypes.Any(st => st.Id == skinTypeId));
            }
            query = query.OrderByDescending(m => m.CreatedAt);
            // Phân trang
            if (page.HasValue && pageSize.HasValue && page > 0 && pageSize > 0)
            {
                query = query
                    .Skip((page.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
            }

            return await query.ToListAsync();
        }

    }
}
