using BusinessLogicLayer.Commons;
using BusinessLogicLayer.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Models.Feedback;

namespace Web.Controllers
{
    [Authorize]
    public class FeedbackController : Controller
    {
        private IFeedbackService _service;

        public FeedbackController(IFeedbackService feedbackService)
        {
            this._service = feedbackService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(Guid? id = null)
        {
            if (id != null)
            {
                ViewData["booking_id"] = id;
            }
            return View();
        }

        public async Task<IActionResult> PostFeedback(BookingFeedbackViewModel feedback)
        {
            Console.WriteLine(feedback.TherapistFeedback);
            Console.WriteLine(feedback.ServiceFeedback);

            ServiceResult result = await _service.CreateFeedback(feedback.BookingId, feedback.TherapistRating, feedback.ServiceRating, feedback.TherapistFeedback, feedback.ServiceFeedback);

            if (result.IsFailed)
            {
                return Redirect(Url.Action("Create","Feedback") + $"?id={feedback.BookingId}");
            }

            return Redirect("/Bookings/History");
        }
    }
}
