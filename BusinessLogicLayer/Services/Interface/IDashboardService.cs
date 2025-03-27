using BusinessObject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.Interface
{
    public interface IDashboardService
    {
        // Lấy tổng số lượng
        Task<int> GetUserCountAsync();
        Task<int> GetServiceCountAsync();
        Task<int> GetBlogCountAsync();
        Task<int> GetTransactionCountAsync();
        Task<int> GetFeedbackCountAsync();
        Task<int> GetBookingCountAsync(DateTime? fromDate, DateTime? toDate);

        // Lấy danh sách tất cả (không phân trang)
        Task<List<User>> GetAllUsersAsync();
        Task<List<Service>> GetAllServicesAsync();
        Task<List<Blog>> GetAllBlogsAsync();
        Task<List<Transaction>> GetAllTransactionsAsync();
        Task<List<Feedback>> GetAllFeedbacksAsync();
        Task<List<Booking>> GetAllBookingsAsync(DateTime? fromDate, DateTime? toDate);

        // Lấy danh sách có phân trang
        Task<List<User>> GetUsersPagedAsync(int pageNumber, int pageSize);
        Task<List<Service>> GetServicesPagedAsync(int pageNumber, int pageSize);
        Task<List<Blog>> GetBlogsPagedAsync(int pageNumber, int pageSize);
        Task<List<Transaction>> GetTransactionsPagedAsync(int pageNumber, int pageSize);
        Task<List<Feedback>> GetFeedbacksPagedAsync(int pageNumber, int pageSize);
        Task<List<Booking>> GetBookingsPagedAsync(DateTime? fromDate, DateTime? toDate, int pageNumber, int pageSize);
    }
}
