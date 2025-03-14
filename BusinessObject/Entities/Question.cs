using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Entities
{
    [Table("Question")]
    public class Question : BaseEntity
    {
        [Column("order_no")]
        public int? OrderNo { get; set; }

        [Column("content")]
        public string? Content { get; set; }

        public virtual ICollection<QuestionOption> QuestionOptions{ get;set; } = new List<QuestionOption>();
    }
}
