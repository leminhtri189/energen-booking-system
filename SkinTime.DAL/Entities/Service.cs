using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using SkinTime.DAL.Enum;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinTime.DAL.Entities
{
    public class Service : BaseEntity
    {
        [Column("service_name", TypeName = "NVARCHAR")]
        [MaxLength(50)]
        public required string ServiceName { get; set; } = string.Empty;
        [Column("description")]
        public string Description { get; set; } = string.Empty;
        [Column("duration")]
        public int Duration { get; set; }
        [Column("thumbnail")]
        public string Thumbnail { get; set; } = string.Empty;
        [Column("price",TypeName = "Decimal")]
        [Precision(16,2)]
        public decimal Price { get; set; }
        [Column("status")]
        public ServiceStatus? Status { get; set; }

        [ForeignKey(nameof(ServiceCategory)), Column("service_category_id")]
        public Guid ServiceCategoryID { get; set; }

        // Virtual properties for relationship navigation
        public virtual ServiceCategory? ServiceCategory { get; set; }
        public virtual ICollection<ServiceRecommendation> ServiceRecommendationNavigation { get; set; } = new Collection<ServiceRecommendation>();
        public virtual ICollection<ServiceImage> ServiceImageNavigation { get; set; } = new Collection<ServiceImage>();
    }
}
