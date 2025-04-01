using BusinessObject.Entities;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.Interface
{
    public interface IBookingService
    {
        Task<IDictionary<TimeOnly, bool>> GetTherapistSchedule(Guid therapistId, string date);
        Task<string> RequestPayment(Guid userId, Booking booking, string returnAction);
    }
}
