using BusinessObject.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Entities
{
    [Table("transaction")]
    public class Transaction : BaseEntity
    {
        [Column("transaction_time")]
        public DateTime? TransactionTime { get; set; }

        [Column("amount", TypeName = "Decimal(16,2)")]
        public decimal? Amount { get; set; }

        [Column("is_refund")]
        public bool Status { get; set; }

        [Column("payment_method")]
        public PaymentMethod PaymentMethod { get; set; }

        [ForeignKey(nameof(Booking)),Column("booking_id")]
        public Guid BookingId { get; set; }

        public virtual Booking BookingNavigation { get; set; } = null!;
    }
}
