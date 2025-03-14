using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Entities
{
    public class BaseEntity
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt{ get; set; } = DateTime.UtcNow;      

        [Column("last_update")]
        public DateTime LastUpdate { get; set; } = DateTime.UtcNow;
    }
}
