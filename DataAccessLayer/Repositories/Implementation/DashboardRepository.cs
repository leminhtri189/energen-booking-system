using BusinessObject.Entities;
using DataAccessLayer.Context;
using DataAccessLayer.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories.Implementation
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext _context;

        public DashboardRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Tổng số lượng
        public async Task<int> GetUserCountAsync()
        {
            return await _context.Users.CountAsync();
        }

        public async Task<int> GetServiceCountAsync()
        {
            return await _context.Services.CountAsync();
        }

        public async Task<int> GetBlogCountAsync()
        {
            return await _context.Blogs.CountAsync();
        }

        public async Task<int> GetTransactionCountAsync()
        {
            return await _context.BookingTransactions.CountAsync();
        }

        public async Task<int> GetFeedbackCountAsync()
        {
            return await _context.Feedbacks.CountAsync();
        }

        public async Task<int> GetBookingCountAsync(DateTime? fromDate, DateTime? toDate)
        {
            var query = _context.Bookings.AsQueryable();
            if (fromDate.HasValue)
                query = query.Where(b => b.CreatedAt >= fromDate.Value);
            if (toDate.HasValue)
                query = query.Where(b => b.CreatedAt <= toDate.Value);
            return await query.CountAsync();
        }

        // Lấy danh sách tất cả
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<List<Service>> GetAllServicesAsync()
        {
            return await _context.Services.ToListAsync();
        }

        public async Task<List<Blog>> GetAllBlogsAsync()
        {
            return await _context.Blogs.ToListAsync();
        }

        public async Task<List<Transaction>> GetAllTransactionsAsync()
        {
            return await _context.BookingTransactions.ToListAsync();
        }

        public async Task<List<Feedback>> GetAllFeedbacksAsync()
        {
            return await _context.Feedbacks.ToListAsync();
        }

        public async Task<List<Booking>> GetAllBookingsAsync(DateTime? fromDate, DateTime? toDate)
        {
            var query = _context.Bookings.AsQueryable();
            if (fromDate.HasValue)
                query = query.Where(b => b.CreatedAt >= fromDate.Value);
            if (toDate.HasValue)
                query = query.Where(b => b.CreatedAt <= toDate.Value);
            return await query.ToListAsync();
        }

        // Lấy danh sách có phân trang
        public async Task<List<User>> GetUsersPagedAsync(int pageNumber, int pageSize)
        {
            return await _context.Users
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<Service>> GetServicesPagedAsync(int pageNumber, int pageSize)
        {
            return await _context.Services
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<Blog>> GetBlogsPagedAsync(int pageNumber, int pageSize)
        {
            return await _context.Blogs
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<Transaction>> GetTransactionsPagedAsync(int pageNumber, int pageSize)
        {
            return await _context.BookingTransactions
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<Feedback>> GetFeedbacksPagedAsync(int pageNumber, int pageSize)
        {
            return await _context.Feedbacks
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<Booking>> GetBookingsPagedAsync(DateTime? fromDate, DateTime? toDate, int pageNumber, int pageSize)
        {
            var query = _context.Bookings.AsQueryable();
            if (fromDate.HasValue)
                query = query.Where(b => b.CreatedAt >= fromDate.Value);
            if (toDate.HasValue)
                query = query.Where(b => b.CreatedAt <= toDate.Value);

            return await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
