using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Entities;
using DataAccessLayer.Context;
using BusinessLogicLayer.Services.Interface;
using DataAccessLayer.Commons;

namespace Web.Controllers
{
    public class TherapistsController : Controller
    {
        private readonly ITherapistService _service;

        public TherapistsController(ITherapistService therapistService)
        {
            _service = therapistService;
        }

        // GET: Therapists
        public async Task<IActionResult> Index(int page = 1, string? searchTerm = null)
        {
            PaginationResult<Therapist> result = await _service.GetTherapistPaginatedAsync(searchTerm, page, 10);
            ViewData["therapists"] = result;
            return View();
        }

        public async Task<IActionResult> Details(Guid therapist_id)
        {
            ViewData["therapists"] = await _service.GetTherapistWithIdAsync(therapist_id);
            return View(await _service.GetTherapistWithIdAsync(therapist_id));
        }

    }
}
