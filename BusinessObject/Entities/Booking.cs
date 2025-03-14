using BusinessObject.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Entities
{
    [Table("Booking")]
    public class Booking : BaseEntity
    {
        [ForeignKey(nameof(User)),Column("user_id")]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(Service)),Column("service_id")]
        public Guid ServiceId { get; set; }

        [ForeignKey(nameof(Therapist)),Column("therapist_id")]
        public Guid? TherapistId { get; set; }

        [Column("reserved_date")]
        public DateTime ReservedDate{ get; set; }

        [Column("total_payment", TypeName = "Decimal(16,2)")]
        public decimal TotalPayment { get; set; }

        [Column("status")]
        public BookingStatus Status { get; set; } = BookingStatus.NotStarted;

        [Column("reserved_start_time")]
        public DateTime ReservedStartTime { get; set; }

        [Column("reserved_end_time")]
        public DateTime ReservedEndTime { get; set; }

        [Column("checkin_time")]
        public DateTime? CheckinTime { get; set; }

        [Column("checkout_time")]
        public DateTime? CheckoutTime { get; set; }

        [Column("note")]
        public string Note { get; set; } = string.Empty;

        public virtual User CustomerNavigation { get; set; } = null!;

        public virtual Transaction? TransactionNavigation { get; set; } = null!;

        public virtual Feedback? FeedbackNavigation { get; set; } = null!;

        public virtual Therapist TherapistNavigation { get; set; } = null!;

        public virtual Service ServiceNavigation { get; set; } = null!;
    }
}
