using BusinessObject.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Entities
{
    [Table("Blog")]
    public class Blog: BaseEntity
    {
        [Column("title", TypeName = "VARCHAR(200)")]
        public string Title { get; set; } = string.Empty;

        [Column("content", TypeName = "VARCHAR(2000)")]
        public string Content { get; set; } = string.Empty;
        [Column("thumbnail", TypeName = "VARCHAR(200)")]
        public string Thumbnail { get; set; } = string.Empty;

        [ForeignKey(nameof(UserNavigation)), Column("author_id")]
        public Guid AuthorId { get; set; }

        [Column("status")]
        public BlogStatus Status { get; set; }

        public virtual User UserNavigation { get; set; } = null!;
    }
}
