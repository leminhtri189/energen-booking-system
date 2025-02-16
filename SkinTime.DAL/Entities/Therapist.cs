using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinTime.DAL.Entities
{
    public class Therapist : BaseEntity
    {
        [Column("experience_years")]
        public int? ExperienceYears { get; set; }
        [Column("bio")]
        public string? BIO { get; set; }
        [Column("status")]
        public string? Status { get; set; }

        [ForeignKey("User"),Column("user_id")]
        public Guid UserID {  get; set; }
        public virtual User? Users{ get; set; }

        public virtual ICollection<Tracking> TrackingNavigation { get; set; } = new List<Tracking>();
    }
}
