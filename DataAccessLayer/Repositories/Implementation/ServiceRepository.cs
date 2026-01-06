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
using Microsoft.AspNetCore.Http;
using Shared.File;

namespace DataAccessLayer.Repositories.Implementation
{
    public class ServiceRepository : GenericRepository<Service>, IServiceRepository
    {
        public ServiceRepository(ApplicationDbContext context) : base(context) { }

        public Task CreateService(Service service, IFormFile thumbnail, ICollection<IFormFile> serviceImage, List<Guid> SkinTypeIds)
        {
            throw new NotImplementedException();
        }

        //public async Task CreateService(Service service, IFormFile thumbnail, ICollection<IFormFile> serviceImages, List<Guid> skinTypeIds)
        //{
        //    service.Id = Guid.NewGuid();

        //    service.Thumbnail = await _firebaseStorage.Upload(thumbnail);

        //    await ((ApplicationDbContext)context).Services.AddAsync(service);
        //    await ((ApplicationDbContext)context).SaveChangesAsync();

        //    if (serviceImages != null && serviceImages.Any())
        //    {
        //        var imageEntities = new List<ServiceImage>();

        //        foreach (var file in serviceImages)
        //        {
        //            if (file.Length > 0)
        //            {
        //                string fileUrl = await _firebaseStorage.Upload(file);
        //                imageEntities.Add(new ServiceImage
        //                {
        //                    ImageUrl = fileUrl,
        //                    ServiceId = service.Id
        //                });
        //            }
        //        }

        //        if (imageEntities.Count > 0)
        //        {
        //            await ((ApplicationDbContext)context).ServiceImages.AddRangeAsync(imageEntities);
        //        }
        //    }

        //    if (skinTypeIds != null && skinTypeIds.Any())
        //    {
        //        var skinTypes = await ((ApplicationDbContext)context).SkinTypes
        //                             .Where(st => skinTypeIds.Contains(st.Id))
        //                             .ToListAsync();

        //        service.SkinTypes = skinTypes;
        //    }

        //    await context.SaveChangesAsync();
        //}



        public async Task<PaginationResult<Service>> GetServices(string? searchKey, Guid? categoryId, Guid? skinTypeId, int? page, int? pageSize)
        {
            IQueryable<Service> query = context.Set<Service>()
                .Include(m => m.ServiceCategoryNavigation)
                .Include(m => m.SkinTypes)
                .OrderByDescending(m => m.CreatedAt);

            if (!string.IsNullOrEmpty(searchKey))
            {
                query = query.Where(m => m.ServiceName.Contains(searchKey));
            }

            if (categoryId.HasValue)
            {
                query = query.Where(m => m.ServiceCategoryId == categoryId);
            }

            if (skinTypeId.HasValue)
            {
                query = query.Where(m => m.SkinTypes.Any(st => st.Id == skinTypeId));
            }
            int totalItemCount = await query.CountAsync();

            int totalPage = (page.HasValue && pageSize.HasValue && pageSize > 0)
                ? (int)Math.Ceiling((double)totalItemCount / pageSize.Value)
                : 1;

            if (page.HasValue && pageSize.HasValue && page > 0 && pageSize > 0)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }

            var pageContent = await query.ToListAsync();

            return new PaginationResult<Service>
            {
                TotalPage = totalPage,
                CurrentPage = page ?? 1,
                PageSize = pageSize ?? totalItemCount,
                TotalItemCount = totalItemCount,
                PageContent = pageContent
            };
        }


    }
}
