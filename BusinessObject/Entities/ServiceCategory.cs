using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Entities
{
    [Table("ServiceCategory")]
    public class ServiceCategory: BaseEntity
    {
        [Column("name")]
        public string? Name { get; set; }

        [Column("status")]
        public string? Status{ get; set; }

        public virtual ICollection<Service>? ServiceNavigation { get; set; }
    }
}
