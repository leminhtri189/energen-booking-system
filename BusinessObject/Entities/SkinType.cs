using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Entities
{
    [Table("SkinType")]
    public class SkinType : BaseEntity
    {
        [Column("name")]
        public string? Name { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        public virtual ICollection<QuestionOption> QuestionOptionNavigation { get; set; } = new List<QuestionOption>();

        public virtual ICollection<Service> Services { get; set; } = new List<Service>();
    }
}
