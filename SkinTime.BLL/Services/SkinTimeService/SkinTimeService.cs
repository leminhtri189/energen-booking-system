using Microsoft.EntityFrameworkCore;
using SkinTime.DAL.Entities;
using SkinTime.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinTime.BLL.Services.SkinTimeService
{
    public class SkinTimeService : ISkinTimeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public SkinTimeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<(Service, List<(Booking, User, Feedback)>)> GetService(string id)
        {
            if (Guid.TryParse(id, out var idService))
            {
                // Lấy thông tin Service
                var service = await _unitOfWork.Repository<Service>().GetByConditionAsync(
                    filter: s => s.Id == idService,
                    includeProperties: query => query
                        .Include(s => s.ServiceDetailNavigation)
                        .Include(s => s.ServiceImageNavigation)
                );

                if (service == null)
                    return (null, null);

                var listBooking = await _unitOfWork.Repository<Booking>().ListAsync(
                    filter: b => b.ServiceId == idService,
                    includeProperties: query => query
                        .Include(b => b.CustomerNavigation) 
                        .Include(b => b.FeedbackNavigation) 
                );
                var result = listBooking.Select(b => (b, b.CustomerNavigation, b.FeedbackNavigation)).ToList();

                return (service, result);
            }

            return (null, null);
        }




    }
}
