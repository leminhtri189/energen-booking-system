using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using SkinTime.DAL.Enum;

namespace SkinTime.DAL.Entities
{
    public class Tracking: BaseEntity
    {
        [ForeignKey(nameof(Schedule)),Column("schedule_id")]
        public Guid ScheduleId { get; set; }

        [ForeignKey(nameof(Therapist)),Column("therapist_id")]
        public Guid TherapistId { get; set; }
        [Column("check_in_time")]
        public DateTime CheckinTime { get; set; }
        [Column("check_out_time")]
        public DateTime CheckoutTime{ get; set; }
        [Column("note")]
        public string Note { get; set; } = string.Empty;

        // Virtual properties
        public virtual Schedule ScheduleNavigation { get; set; } = null!;
        public virtual Therapist TherapistNavigation { get; set; } = null!;
    }
}