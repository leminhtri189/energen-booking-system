using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinTime.DAL.Entities
{
    public class QuestionOption : BaseEntity
    {
        [Column("content")]
        public string? Content {  get; set; }
        [Column("is_deleted")]
        public bool? IsDeleted { get; set; }
        [ForeignKey("SkinType"),Column("skin_type_id")]
        public Guid SkinTypeID { get; set; }
        public virtual SkinType? SkinType { get; set; }
        [Column("question_id")]
        public Guid QuestionID { get; set; }
    }
}
