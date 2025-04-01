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
using Microsoft.AspNetCore.SignalR;
using Web.Hubs;

namespace Web.Controllers
{
    public class BookingsController : Controller
    {
        private readonly ISkinTimeService _skinTimeService;
        private readonly ITherapistService _therapistService;
        private readonly IBookingService _bookingService;
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
        public async Task<IActionResult> BookingService([FromForm] BookingServiceViewModel bookingViewModel)
        {
            Guid userId = Guid.Parse("86DA4A86-3056-4E27-A352-F8F3187EBC41");
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
        public async Task<IActionResult> GetTherapists(int page = 1, int pageSize = 4, string? searchKey = null)
        {
            var therapists = await _therapistService.GetTherapists(searchKey, page, pageSize);

            if (therapists == null || !therapists.Any())
            {
                return Content("");
            }

            return PartialView("_TherapistPartial", therapists);
        }

        public async Task<IActionResult> BookingTracking()
        {
            int role = 1;// staff
            bool result;
            if (role ==1)
            {
                result = true;
            }
            var userId = Guid.Parse("CF57BFEE-0396-4FF4-8791-AD998D1125C0");
            var listBooking = await _bookingService.GetBookingTracking(true,userId);
            return View("ManageBooking", listBooking);
        }
        public async Task<IActionResult> Checkin(Guid bookingId, string checkinCode)
        {
            //var booking = await _bookingService.GetBookingById(bookingId);
            //if (booking == null) return BadRequest("Booking không tồn tại!");

            //string generatedCode = booking.BookingId.ToString("N").Substring(0, 6).ToUpper();
            //if (generatedCode != checkinCode.ToUpper())
            //    return BadRequest("Mã check-in không đúng!");

            //booking.CheckinTime = DateTime.Now;
            //await _bookingService.UpdateBooking(booking);

            //// Gửi tín hiệu cập nhật đến tất cả client
            //await _hubContext.Clients.All.SendAsync("ReceiveBookingUpdate", booking.BookingId);
            return Ok();
        }
        public async Task<IActionResult> SaveNote(Guid bookingId, string note)
        {
            //var booking = await _bookingService.GetBookingById(bookingId);
            //if (booking == null) return BadRequest("Booking không tồn tại!");

            //booking.Note = note;
            //await _bookingService.UpdateBooking(booking);

            //// Gửi tín hiệu cập nhật đến tất cả client
            //await _hubContext.Clients.All.SendAsync("ReceiveBookingUpdate", booking.BookingId);
            return Ok();
        }
        public async Task<IActionResult> Checkout(Guid bookingId)
        {
            //var booking = await _bookingService.GetBookingById(bookingId);
            //if (booking == null) return BadRequest("Booking không tồn tại!");

            //if (string.IsNullOrEmpty(booking.Note))
            //    return BadRequest("Vui lòng nhập ghi chú trước khi check-out!");

            //booking.CheckoutTime = DateTime.Now;
            //await _bookingService.UpdateBooking(booking);

            //// Gửi tín hiệu cập nhật đến tất cả client
            //await _hubContext.Clients.All.SendAsync("ReceiveBookingUpdate", booking.BookingId);
            return Ok();
        }
    }
}
