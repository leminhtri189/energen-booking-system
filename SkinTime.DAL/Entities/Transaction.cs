using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace SkinTime.DAL.Entities
{
    public class Transaction : BaseEntity
    {
        [Column("description")]
        public string? Description { get; set; }
        [Column("status")]
        public string? Status { get; set; }
        [Column("transaction_time")]
        public DateTime? TransactionTime { get; set; }
        [Column("amount", TypeName = "Decimal")]
        [Precision(16, 2)]
        public decimal? Amount { get; set; }
        [Column("payment_method")]
        public string? PaymentMethod { get; set; }
        [ForeignKey("Booking"),Column("booking_id")]
        public virtual Guid? BookingId { get; set; }
        public virtual Booking? Booking { get; set; }
    }
}
