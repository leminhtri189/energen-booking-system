using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Entities;
using DataAccessLayer.Context;
using DataAccessLayer.Repositories.Interface;
using AutoMapper;
using BusinessLogicLayer.Services.Interface;
using Web.Models;

namespace Web.Controllers
{
    public class BookingsController : Controller
    {
        private readonly ISkinTimeService _skinTimeService;
        private readonly ITherapistService _therapistService;
        private readonly IBookingService _bookingService;
        private readonly IMapper _mapper;
        public string? SearchQuery { get; set; }
        public int? PageNumber { get; set; } = 1;
        public int? PageSize { get; set; } = 4;
        public BookingsController(ITherapistService therapistService, ISkinTimeService skinTimeService, IBookingService bookingService,IMapper mapper)
        {
            _skinTimeService = skinTimeService;
            _mapper = mapper;
            _therapistService = therapistService;
            _bookingService = bookingService;   
        }

        // GET: Bookings
        public async Task<IActionResult> Index(Guid id, string? searchKey, int? page)
        {
            int pageNumber = page ?? 1;

            var service = await _skinTimeService.GetService(id);
            var listTherapist = await _therapistService.GetTherapists(SearchQuery, pageNumber, PageSize);
            var viewModel = new BookingViewModel
            {
                Service = service,
                Therapists = listTherapist // Truyền danh sách chuyên viên trực tiếp
            };

            return View(viewModel);
        }
        public async Task<IActionResult> BookingService([FromBody] BookingServiceViewModel bookingViewModel)
        {
            Guid userId = Guid.Parse("86DA4A86-3056-4E27-A352-F8F3187EBC41");
          var booking = _mapper.Map<Booking>(bookingViewModel);
            var returnAction = Url.Action("TransactionCallback", "Transaction", null, Request.Scheme);
            var requestPayment = await _bookingService.RequestPayment(userId, booking, returnAction);
            return Json(new { redirectUrl = requestPayment });
        }
        public IActionResult GetAvailableTimeSlots(Guid therapistid, string date)
        {

            var timeSlots = _bookingService.GetTherapistSchedule(therapistid, date);
            return Json(new { success = true, data = timeSlots });
        }

    }
}
