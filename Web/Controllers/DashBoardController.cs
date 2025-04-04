using BusinessLogicLayer.Services.Interface;
using BusinessObject.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Controllers
{
    public class DashBoardController : Controller
    {
        public readonly IDashboardService _dashboardService;
        public DashBoardController(IDashboardService dashboardService  )
        {
            _dashboardService = dashboardService;
        }

        public async Task<IActionResult> Index(DateOnly? date,int? pageNumber)
        {
            var currentMonth = date?.ToString("yyyy-MM") ?? DateTime.Now.ToString("yyyy-MM");


            ViewData["SelectedMonth"] = currentMonth;
            var booking = await _dashboardService.GetBookingsPagedAsync(date, pageNumber);
            var transaction = await _dashboardService.GetTransactionsPagedAsync(date, pageNumber);
            var services = await _dashboardService.GetServicesPagedAsync(date, pageNumber);
            var user = await _dashboardService.GetUsersPagedAsync(date, pageNumber);
            var (labels, bookingsData) = await _dashboardService.GetBookingChartDataAsync(date);
            var viewModel = new DashBoardViewModel
            {
                Services = services,
                Bookings = booking,
                Transactions = transaction,
                Users = user,
                BookingChart = new BookingChartViewModel
                {
                    Labels = labels,         
                    BookingsData = bookingsData, 
                                               
                }
            };
            return View(viewModel);
        }
        // Booking Pagination with Month filter
        //public async Task<IActionResult> GetBookingsPaged(int pageNumber = 1, DateOnly month = null)
        //{
        //    var bookings = await _dashboardService.GetBookingsPagedAsync(month,pageNumber);
        //    var viewModel = new DashBoardViewModel
        //    {
        //        Bookings = bookings
        //    };
        //    return PartialView("_BookingsList", viewModel);
        //}

        // Transaction Pagination with Month filter
        //public async Task<IActionResult> GetTransactionsPaged(int pageNumber = 1, string month = null)
        //{
        //    var transactions = await _dashboardService.GetTransactionsPagedAsync(pageNumber, month);
        //    var viewModel = new DashBoardViewModel
        //    {
        //        Transactions = transactions
        //    };
        //    return PartialView("_TransactionsList", viewModel);
        //}

        //// Services Pagination with Month filter
        //public async Task<IActionResult> GetServicesPaged(int pageNumber = 1, string month = null)
        //{
        //    var services = await _dashboardService.GetServicesPagedAsync(pageNumber, month);
        //    var viewModel = new DashBoardViewModel
        //    {
        //        Services = services
        //    };
        //    return PartialView("_ServicesList", viewModel);
        //}

        //// Users Pagination with Month filter
        //public async Task<IActionResult> GetUsersPaged(int pageNumber = 1, string month = null)
        //{
        //    var users = await _dashboardService.GetUsersPagedAsync(pageNumber, month);
        //    var viewModel = new DashBoardViewModel
        //    {
        //        Users = users
        //    };
        //    return PartialView("_UsersList", viewModel);
        //}
    }
}
