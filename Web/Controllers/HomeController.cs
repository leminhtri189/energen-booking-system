using AutoMapper;
using BusinessLogicLayer.Services.Interface;
using BusinessObject.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
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
            var countService = await _dashboardService.GetServiceCountAsync();
            var countUser = await _dashboardService.GetCustomerCountAsync();
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
