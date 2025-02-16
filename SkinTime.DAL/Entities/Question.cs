using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinTime.DAL.Entities
{
    public class Question : BaseEntity
    {
        [Column("order_no")]
        public int? OrderNo { get; set; }
        [Column("content")]
        public string? Content { get; set; }
        public virtual ICollection<QuestionOption> QuestionOptions{ get;set; }
    }
}
