using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Entities
{
    [Table("ServiceImage")]
    public class ServiceImage : BaseEntity
    {
        [Column("image_url")]
        public string? ImageUrl { get; set; }

        [ForeignKey(nameof(Service)), Column("service_id ")]
        public Guid? ServiceId {  get; set; }

        public virtual Service ServiceNavigation { get; set; } = null!;
    }
}
