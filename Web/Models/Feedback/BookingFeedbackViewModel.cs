using System.ComponentModel.DataAnnotations;

namespace Web.Models.Feedback
{
    public class BookingFeedbackViewModel
    {
        public Guid BookingId { get; set; }
        [Required]
        public int TherapistRating { get; set; }

        [Required]
        public string TherapistFeedback { get; set; }

        [Required]
        public int ServiceRating { get; set; }

        [Required]
        public string ServiceFeedback { get; set; }
    }
}
