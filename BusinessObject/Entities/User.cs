using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using BusinessObject.Enums;

namespace BusinessObject.Entities
{
    [Table("User")]
    public class User : BaseEntity
    {
        [Column("fullname", TypeName = "VARCHAR(50)")]
        public string FullName { get; set; } = string.Empty;

        [Column("username", TypeName = "VARCHAR(40)")]
        public string UseName { get; set; } = string.Empty;

        [Column("email", TypeName = "VARCHAR(35)")]
        public string Email { get; set; } = string.Empty;

        [Column("password", TypeName = "VARCHAR(250)")]
        public string Password { get; set; } = string.Empty;

        [Column("gender")]
        public Gender Gender { get; set; }

        [Column("phone_number", TypeName = "VARCHAR(11)")]
        public string Phone { get; set; } = string.Empty;

        [Column("role")]
        public Role Role { get; set; }

        [Column("status")]
        public UserStatus Status { get; set; }

        public virtual Therapist Therapists { get; set; } = null!;

        public virtual ICollection<Booking> BookingNavigation { get; set; } = new List<Booking>();

        public virtual ICollection<Blog> BlogNavigation {  get; set; } = new List<Blog>();
    }
}
