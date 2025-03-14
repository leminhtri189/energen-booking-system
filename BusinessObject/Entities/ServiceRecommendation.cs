using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Entities
{
    [Table("ServiceRecommendation")]
    public class ServiceRecommendation : BaseEntity
    {
        [ForeignKey("Service"), Column("service_id")]
        public Guid ServiceId { get; set; }

        
        [ForeignKey(nameof(SkinType)), Column("skin_type_id")]
        public Guid SkinTypeId { get; set; }

        public virtual SkinType? SkinTypeNavigation { get; set; }
        
        public virtual Service? ServiceNavigation { get; set; }
    }
}
