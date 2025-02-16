using Microsoft.EntityFrameworkCore;
using SkinTime.DAL.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinTime.DAL.Entities
{
    public class Booking : BaseEntity
    {
        [ForeignKey(nameof(User)),Column("user_id")]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(Service)),Column("service_id")]
        public Guid ServiceId { get; set; }

        /* 
         * Nullable vì ban đầu nếu khách không chọn Therapist thì sẽ không có Id của chuyên viên.
         * Staff (nhân viên) sẽ là người xếp chuyên viên vào các buổi làm việc.
         * - Giang
         */
        [ForeignKey(nameof(Therapist)),Column("therapist_id")]
        public Guid? TherapistId { get; set; }
        [Column("reserved_date")]
        public DateTime ReservedDate{ get; set; }
        [Column("total_payment", TypeName = "Decimal")]
        [Precision(16,2)]
        public decimal TotalPayment { get; set; }
        [Column("status")]
        public BookingStatus Status { get; set; } = BookingStatus.NotStarted;

        // Virtual properties
        public virtual User CustomerNavigation { get; set; } = null!;
        public virtual Therapist TherapistNavigation { get; set; } = null!;
        public virtual Feedback? FeedbackNavigation { get; set; } = null!;
        public virtual Service ServiceNavigation { get; set; } = null!;
        public virtual ICollection<Schedule> ScheduleNavigation { get; set; } = new List<Schedule>();
    }
}
