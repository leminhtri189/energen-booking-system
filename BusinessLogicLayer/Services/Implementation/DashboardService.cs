using BusinessLogicLayer.Services.Interface;
using BusinessObject.Entities;
using BusinessObject.Enums;
using DataAccessLayer.Repositories.Interface;
using DataAccessLayer.UoW;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
namespace BusinessLogicLayer.Services.Implementation
{
    public class DashboardService : IDashboardService
    {
        private IUnitOfWork _unitOfWork;
        private IDashboardRepository dashboardRepository;

        public DashboardService(IDashboardRepository dashboardRepository, IUnitOfWork unitOfWork)
        {
            this.dashboardRepository = dashboardRepository;
            _unitOfWork = unitOfWork;
        }

        // Tổng số lượng
        public async Task<int> GetCustomerCountAsync()
        {
            var listUser = await _unitOfWork.Users.GetAllAsync(us => us.Role == Role.Custommer);
            return listUser.Count();
        }

        public async Task<int> GetTherapistCountAsync()
        {
            var listUser = await _unitOfWork.Users.GetAllAsync(us => us.Role == Role.Therapist);
            return listUser.Count();
        }

        public async Task<int> GetServiceCountAsync()
        {
            var listService = await _unitOfWork.Services.GetAllAsync(se => se.Status == ServiceStatus.Available);
            return listService.Count();
        }

        public async Task<int> GetBlogCountAsync()
        {
            return await dashboardRepository.GetBlogCountAsync();
        }

        public async Task<int> GetTransactionCountAsync()
        {
            return await dashboardRepository.GetTransactionCountAsync();
        }

        public async Task<int> GetFeedbackCountAsync()
        {
            return await dashboardRepository.GetFeedbackCountAsync();
        }

        public async Task<int> GetBookingCountAsync(DateTime? fromDate, DateTime? toDate)
        {
            return await dashboardRepository.GetBookingCountAsync(fromDate, toDate);
        }

        // Lấy danh sách tất cả
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await dashboardRepository.GetAllUsersAsync();
        }

        public async Task<List<Service>> GetAllServicesAsync()
        {
            return await dashboardRepository.GetAllServicesAsync();
        }

        public async Task<List<Blog>> GetAllBlogsAsync()
        {
            return await dashboardRepository.GetAllBlogsAsync();
        }

        public async Task<List<Transaction>> GetAllTransactionsAsync()
        {
            return await dashboardRepository.GetAllTransactionsAsync();
        }

        public async Task<List<Feedback>> GetAllFeedbacksAsync()
        {
            return await dashboardRepository.GetAllFeedbacksAsync();
        }

        public async Task<List<Booking>> GetAllBookingsAsync(DateTime? fromDate, DateTime? toDate)
        {
            return await dashboardRepository.GetAllBookingsAsync(fromDate, toDate);
        }

        // Lấy danh sách có phân trang
        public async Task<List<User>> GetUsersPagedAsync(int pageNumber, int pageSize)
        {
            return await dashboardRepository.GetUsersPagedAsync(pageNumber, pageSize);
        }

        public async Task<List<Service>> GetServicesPagedAsync(int pageNumber, int pageSize)
        {
            return await dashboardRepository.GetServicesPagedAsync(pageNumber, pageSize);
        }

        public async Task<List<Blog>> GetBlogsPagedAsync(int pageNumber, int pageSize)
        {
            return await dashboardRepository.GetBlogsPagedAsync(pageNumber, pageSize);
        }

        public async Task<List<Transaction>> GetTransactionsPagedAsync(int pageNumber, int pageSize)
        {
            return await dashboardRepository.GetTransactionsPagedAsync(pageNumber, pageSize);
        }

        public async Task<List<Feedback>> GetFeedbacksPagedAsync(int pageNumber, int pageSize)
        {
            return await dashboardRepository.GetFeedbacksPagedAsync(@pageNumber, pageSize);
        }

        public async Task<List<Booking>> GetBookingsPagedAsync(DateTime? fromDate, DateTime? toDate, int pageNumber, int pageSize)
        {
            return await dashboardRepository.GetBookingsPagedAsync(fromDate, toDate, pageNumber, pageSize);
        }

    }
}
