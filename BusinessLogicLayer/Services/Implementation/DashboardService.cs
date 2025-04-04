using BusinessLogicLayer.Services.Interface;
using BusinessObject.Entities;
using BusinessObject.Enums;
using DataAccessLayer.Commons;
using DataAccessLayer.Repositories.Interface;
using DataAccessLayer.UoW;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;

namespace BusinessLogicLayer.Services.Implementation
{
    public class DashboardService : IDashboardService
    {
        private IUnitOfWork _unitOfWork;

        public DashboardService( IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<(string[] Labels, int[] BookingsData)> GetBookingChartDataAsync(DateOnly? date)
        {
            // Lấy tất cả bookings từ repository (nếu cần có thể thêm filter)
            var bookings = await _unitOfWork.GenericRepository<Booking>().GetAllAsync();

            // Chuyển IEnumerable<Booking> thành IQueryable<Booking>
            IQueryable<Booking> bookingsQuery = bookings.AsQueryable();

            if (date.HasValue)
            {
                DateOnly targetDate = date.Value;
                bookingsQuery = bookingsQuery.Where(b => b.CreatedAt.Month == targetDate.Month && b.CreatedAt.Year == targetDate.Year);
            }

            // Lấy danh sách bookings sau khi đã filter
            var filteredBookings =  bookingsQuery.ToList();

            // Kiểm tra nếu không có bookings
            if (!filteredBookings.Any())
            {
                return (new string[0], new int[0]);
            }

            // Tìm ngày đầu tiên và cuối cùng trong danh sách bookings
            var startDate = filteredBookings.Min(b => b.CreatedAt.Date);
            var endDate = filteredBookings.Max(b => b.CreatedAt.Date);

            // Tạo mảng labels cho các ngày trong khoảng từ startDate đến endDate
            var totalDays = (endDate - startDate).Days + 1;
            var labels = Enumerable.Range(0, totalDays)
                                   .Select(i => startDate.AddDays(i).ToString("dd/MM/yyyy"))
                                   .ToArray();

            // Tạo mảng bookingsCount cho các ngày trong khoảng thời gian
            var bookingsCount = new int[totalDays];

            // Đếm số lượng bookings cho từng ngày
            foreach (var booking in filteredBookings)
            {
                int dayIndex = (booking.CreatedAt.Date - startDate).Days;
                bookingsCount[dayIndex]++;
            }

            return (labels, bookingsCount);
        }


        public async Task<PaginationResult<Booking>> GetBookingsPagedAsync(DateOnly? date, int? pageNumber)
        {
            int currentPage = pageNumber ?? 1;
            DateOnly targetDate = date ?? DateOnly.FromDateTime(DateTime.Today);
            var itemsCount = await _unitOfWork.GenericRepository<Booking>().GetAllAsync();
            IEnumerable<Booking> items;
            if (date.HasValue)
            {
                items = await _unitOfWork.GenericRepository<Booking>().GetAllAsync(
                    s => s.CreatedAt.Month == targetDate.Month && s.CreatedAt.Year == targetDate.Year
                );
            }
            else
            {
                items = await _unitOfWork.GenericRepository<Booking>().GetAllAsync(); // Lấy tất cả
            }

            int pageSize = 10;
            int totalItemCount = items.Count();
            int totalPage = (int)Math.Ceiling((double)totalItemCount / pageSize);
            var pageContent = items.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            return new PaginationResult<Booking>
            {
                TotalPage = totalPage,
                Totaltem = itemsCount.Count(),
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalItemCount = totalItemCount,
                PageContent = pageContent
            };
        }


        public async Task<PaginationResult<Service>> GetServicesPagedAsync(DateOnly? date, int? pageNumber)
        {
            int currentPage = pageNumber ?? 1;
            var itemsCount = await _unitOfWork.GenericRepository<Service>().GetAllAsync();
            DateOnly targetDate = date ?? DateOnly.FromDateTime(DateTime.Today);
            IEnumerable<Service> items;
            if (date.HasValue)
            {
                items = await _unitOfWork.GenericRepository<Service>().GetAllAsync(
                    s => s.CreatedAt.Month == targetDate.Month && s.CreatedAt.Year == targetDate.Year
                );
            }
            else
            {
                items = await _unitOfWork.GenericRepository<Service>().GetAllAsync(); // Lấy tất cả
            }


            int pageSize = 10;
            int totalItemCount = items.Count();
            int totalPage = (int)Math.Ceiling((double)totalItemCount / pageSize);
            var pageContent = items.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            return new PaginationResult<Service>
            {
                TotalPage = totalPage,
                Totaltem = itemsCount.Count(),
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalItemCount = totalItemCount,
                PageContent = pageContent
            };
        }

        public async Task<int> GetTherapistCountAsync()
        {
            var items = await _unitOfWork.GenericRepository<User>().GetAllAsync(
                 u =>  u.Role == Role.Therapist
             );
            return  items.Count();
        }

        public async Task<PaginationResult<Transaction>> GetTransactionsPagedAsync(DateOnly? date, int? pageNumber)
        {
            int currentPage = pageNumber ?? 1;
            var itemsCount = await _unitOfWork.GenericRepository<Transaction>().GetAllAsync();
            DateOnly targetDate = date ?? DateOnly.FromDateTime(DateTime.Today);
            IEnumerable<Transaction> items;
            if (date.HasValue)
            {
                items = await _unitOfWork.GenericRepository<Transaction>().GetAllAsync(
                    s => s.CreatedAt.Month == targetDate.Month && s.CreatedAt.Year == targetDate.Year
                );
            }
            else
            {
                items = await _unitOfWork.GenericRepository<Transaction>().GetAllAsync(); // Lấy tất cả
            }

            int pageSize = 10;
            int totalItemCount = items.Count();
            int totalPage = (int)Math.Ceiling((double)totalItemCount / pageSize);
            var pageContent = items.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            return new PaginationResult<Transaction>
            {
                TotalPage = totalPage,
                CurrentPage = currentPage,
                Totaltem = itemsCount.Count(),
                PageSize = pageSize,
                TotalItemCount = totalItemCount,
                PageContent = pageContent
            };
        }


        public async Task<PaginationResult<User>> GetUsersPagedAsync(DateOnly? date, int? pageNumber)
        {
            int currentPage = pageNumber ?? 1;

            DateOnly targetDate = date ?? DateOnly.FromDateTime(DateTime.Today);
            var itemsCount = await _unitOfWork.GenericRepository<User>().GetAllAsync();
            IEnumerable<User> items;
            if (date.HasValue)
            {
                items = await _unitOfWork.GenericRepository<User>().GetAllAsync(
                    s => s.CreatedAt.Month == targetDate.Month && s.CreatedAt.Year == targetDate.Year
                );
            }
            else
            {
                items = await _unitOfWork.GenericRepository<User>().GetAllAsync(); 
            }

            int pageSize = 10;
            int totalItemCount = items.Count();
            int totalPage = (int)Math.Ceiling((double)totalItemCount / pageSize);
            var pageContent = items.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            return new PaginationResult<User>
            {
                TotalPage = totalPage,
                CurrentPage = currentPage,
                Totaltem = itemsCount.Count(),
                PageSize = pageSize,
                TotalItemCount = totalItemCount,
                PageContent = pageContent
            };
        }

    }
}
