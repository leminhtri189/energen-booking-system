using AutoMapper;
using BusinessLogicLayer.Services.Interface;
using BusinessObject.Entities;
using BusinessObject.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using Web.Models;
using Web.Models.Home;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDashboardService _dashboardService;
        private readonly IMapper _mapper;

        public HomeController(IDashboardService dashboardService,IMapper mapper)
        {
          _mapper = mapper;
            _dashboardService = dashboardService;
        }

        public async Task<IActionResult> Index()
        {
            string role = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
            if (role =="2" || role == "3")
            {
                return RedirectToAction("ManageBooking", "Bookings");
            }
            var Services = await _dashboardService.GetServicesPagedAsync(null,null);
            var countService = Services.Totaltem;
            var Users = await _dashboardService.GetUsersPagedAsync(null, null);
            var countUser = Users.Totaltem;
            var countTherapist = await _dashboardService.GetTherapistCountAsync();
            var model = new HomeViewModel
            {
                ServiceCount = countService,
                UserCount = countUser,
                TherapistCount = countTherapist
            };

            return View(model);

        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
