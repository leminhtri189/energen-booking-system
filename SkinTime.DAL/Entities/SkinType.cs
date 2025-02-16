using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinTime.DAL.Entities
{
    public class SkinType : BaseEntity
    {
        [Column("name")]
        public string? Name { get; set; }
        [Column("description")]
        public string? Description { get; set; }
        public virtual ICollection<ServiceRecommendation> Recommendations { get; set; }
    }
}
