using BusinessLogicLayer.Services.Implementation;
using BusinessObject.Entities;
using DataAccessLayer.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.Interface
{
    public interface IDashboardService
    {
        // Lấy danh sách có phân trang
        Task<PaginationResult<User>> GetUsersPagedAsync(DateOnly? date, int? pageNumber);
        Task<PaginationResult<Service>> GetServicesPagedAsync(DateOnly? date, int? pageNumber);
        Task<PaginationResult<Transaction>> GetTransactionsPagedAsync(DateOnly? date, int? pageNumber);
        Task<PaginationResult<Booking>> GetBookingsPagedAsync(DateOnly? date ,int? pageNumber);
        Task<int> GetTherapistCountAsync();
        Task<(string[] Labels, int[] BookingsData)> GetBookingChartDataAsync(DateOnly? date);

    }
}
