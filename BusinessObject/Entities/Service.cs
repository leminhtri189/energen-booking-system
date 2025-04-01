using BusinessObject.Enums;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Entities
{
    [Table("Service")]
    public class Service : BaseEntity
    {
        [Column("service_name", TypeName = "NVARCHAR")]
        [MaxLength(50)]
        public required string ServiceName { get; set; } = string.Empty;//
        [Column("description")]
        public string Description { get; set; } = string.Empty;//
        [Column("duration")]
        public int Duration { get; set; }//
        [Column("thumbnail")]
        public string Thumbnail { get; set; } = string.Empty;//
        [Column("price",TypeName = "Decimal")]
        [Precision(16,2)]
        public decimal Price { get; set; }//
        [Column("status")]
        public ServiceStatus Status { get; set; }

        [ForeignKey(nameof(ServiceCategory)), Column("service_category_id")]
        public Guid ServiceCategoryId { get; set; }

        // Virtual properties for relationship navigation
        public virtual ServiceCategory? ServiceCategoryNavigation { get; set; }// NGOAI 
        public virtual ICollection<SkinType> SkinTypes { get; set; } = new Collection<SkinType>();// TRONG
        public virtual ICollection<ServiceImage> ServiceImageNavigation { get; set; } = new Collection<ServiceImage>();// TRONG
    }
}
