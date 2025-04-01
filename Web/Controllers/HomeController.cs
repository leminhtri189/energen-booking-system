using AutoMapper;
using BusinessLogicLayer.Services.Interface;
using BusinessObject.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISkinTimeService _skinTimeService;
        private readonly IMapper _mapper;

        public HomeController(ISkinTimeService skinTimeService,IMapper mapper)
        {
          _mapper = mapper;
            _skinTimeService = skinTimeService;
        }

        public async Task<IActionResult> Index()
        {
            return View();

        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
