using System.ComponentModel.DataAnnotations.Schema;


namespace BusinessObject.Entities
{
    [Table("QuestionOption")]
    public class QuestionOption : BaseEntity
    {
        [Column("content", TypeName = "VARCHAR(200)")]
        public string? Content {  get; set; }

        [Column("is_deleted")]
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(Question)),Column("question_id")]
        public Guid QuestionId { get; set; }

        public virtual QuestionOption QuestionNavigation { get; set; } = null!;

        public virtual ICollection<SkinType> SkinTypeNavigation { get; set; } = null!;
        
    }
}
