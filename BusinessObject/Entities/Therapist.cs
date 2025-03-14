using System.ComponentModel.DataAnnotations.Schema;


namespace BusinessObject.Entities
{
    [Table("Therapist")]
    public class Therapist : BaseEntity
    {
        [Column("experiences")]
        public int? ExperienceYears { get; set; }

        [Column("bio")]
        public string? Biography { get; set; }

        [Column("status")]
        public string? Status { get; set; }

        [ForeignKey(nameof(User)), Column("user_id")]
        public Guid UserId { get; set; }

        public virtual User UserNavigation { get; set; } = null!;

        public virtual ICollection<Booking> BookingNavigation { get; set; } = new List<Booking>();
    }
}
