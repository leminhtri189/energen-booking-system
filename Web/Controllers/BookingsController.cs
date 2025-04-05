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
using Microsoft.AspNetCore.Authorization;
using BusinessObject.Enums;
using DataAccessLayer.Commons;
using Microsoft.AspNetCore.SignalR;
using Web.Hubs;
using System.Security.Claims;

namespace Web.Controllers
{
    public class BookingsController : Controller
    {
        private readonly ISkinTimeService _skinTimeService;
        private readonly ITherapistService _therapistService;
        private readonly IBookingService _bookingService;
        private Guid? _serviceId;
        private readonly IHubContext<BookingHub> _hubContext;
        private readonly IMapper _mapper;
        public string? SearchQuery { get; set; }
        public int? PageNumber { get; set; } = 1;
        public int? PageSize { get; set; } = 4;
        public BookingsController(ITherapistService therapistService, ISkinTimeService skinTimeService, IBookingService bookingService,IMapper mapper, IHubContext<BookingHub> hubContext)
        {
            _skinTimeService = skinTimeService;
            _mapper = mapper;
            _therapistService = therapistService;
            _bookingService = bookingService;   
            _hubContext = hubContext;
        }

        [Authorize(Roles = "4")]
        public IActionResult Index(Guid id, string? searchKey, int? page)
        {


                var service = _skinTimeService.GetService(id); 
                return View(service);

        }

        [Authorize]
        public async Task<IActionResult> History(int page = 1, BookingStatus status = BookingStatus.NotStarted)
        {
            string user_id = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Id")!.Value;
            ViewData["booking_pagination"] = await _bookingService.GetCustomerBookingWithStatus(Guid.Parse(user_id), page, 5, status);
            ViewData["booking_status_selection"] = status;
            return View();
        }
        public async Task<IActionResult> GetTherapistsToBooking(int? page)
        {
            int pageNumber = page ?? 1;
            var therapists = await _therapistService.GetTherapistsToBooking(pageNumber, 4);

            return PartialView("_TherapistPartial", therapists);
        }

        [Authorize(Roles = "4")]
        public async Task<IActionResult> BookingService([FromForm] BookingServiceViewModel bookingViewModel)
        {
            if (!User.Identity.IsAuthenticated) // Kiểm tra nếu chưa đăng nhập
            {
                return RedirectToAction("Login", "Account", new { returnUrl = Request.Path });
            }
            string user_id = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Id")!.Value;
            Guid userId = Guid.Parse(user_id);
          var booking = _mapper.Map<Booking>(bookingViewModel);
            var returnAction = Url.Action("TransactionCallback", "Transaction", null, Request.Scheme);
            var requestPayment = await _bookingService.RequestPayment(userId, booking, returnAction);
            return Redirect(requestPayment);
        }
        public IActionResult GetAvailableTimeSlots(Guid therapistid, string date)
        {

            var timeSlots = _bookingService.GetTherapistSchedule(therapistid, date);
            return Json(new { success = true, data = timeSlots });
        }

        [Authorize(Roles = "2, 3")]
        public async Task<IActionResult> ManageBooking(DateOnly? targetDate,bool? updated)
        {
            string user_id = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Id")!.Value;
            string role = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)!.Value;
            var listBooking = await _bookingService.GetBookingTracking(role, user_id,targetDate);
            return View("ManageBooking", listBooking);
        }
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Checkin(Guid bookingId)
        {
            await _bookingService.CheckIn(bookingId);

            // Gửi tín hiệu SignalR cho các client khác
            await _hubContext.Clients.All.SendAsync("ReceiveBookingUpdate");

            // Chuyển hướng tới trang ManageBooking và thêm tham số 'updated=true' để reload trang
            return RedirectToAction("ManageBooking", "Bookings", new { updated = "true" });
        }

        [Authorize(Roles = "3")]
        public async Task<IActionResult> SaveNote(Guid bookingId, string note)
        {
            await _bookingService.Note(bookingId, note);

            await _hubContext.Clients.All.SendAsync("ReceiveBookingUpdate");

            return RedirectToAction("ManageBooking", "Bookings", new { updated = "true" });
        }

        [Authorize(Roles = "2")]
        public async Task<IActionResult> Checkout(Guid bookingId)
        {
            await _bookingService.CheckOut(bookingId);

            // Chỉ cần gửi tín hiệu SignalR mà không cần thêm tham số 'updated=true' trong URL
            await _hubContext.Clients.All.SendAsync("ReceiveBookingUpdate");

            return RedirectToAction("ManageBooking", "Bookings", new { updated = "true" });
        }

    }
}
