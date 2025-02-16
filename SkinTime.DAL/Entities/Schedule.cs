using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SkinTime.DAL.Enum;

namespace SkinTime.DAL.Entities
{
    public class Schedule : BaseEntity
    {
        [ForeignKey(nameof(Booking)),Column("booking_id")]
        public Guid BookingId{ get; set; }
        [Column("date")]
        public DateTime Date { get; set; }
        [Column("reserved_start_time")]

        public DateTime ReservedStartTime { get; set; }
        [Column("reserved_end_time")]
        public DateTime ReservedEndTime { get; set; }
        [Column("status")]
        public ScheduleStatus Status { get; set; } = ScheduleStatus.NotStarted;

        // Virtual properties
        public virtual Booking BookingNavigation { get; set; } = null!;
        public virtual Tracking? TrakingNavigation {  get; set; }
    }
}