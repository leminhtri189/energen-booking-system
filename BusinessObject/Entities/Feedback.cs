using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Entities
{
    [Table("Feedback")]
    public class Feedback : BaseEntity
    {
        [ForeignKey(nameof(Booking)),Column("booking_id")]
        public Guid BookingId { get; set; }

        [Column("therapist_rating")]
        public int TherapistRating { get; set; }

        [Column("service_rating")]
        public int ServiceRating { get; set; }

        [Column("therapist_feedback")]
        public string TherapistFeedback { get; set; } = string.Empty;

        [Column("service_feedback")]
        public string ServiceFeedback { get; set; } = string.Empty;

        public virtual Booking BookingNavigation { get; set; } = null!;
    }
}
