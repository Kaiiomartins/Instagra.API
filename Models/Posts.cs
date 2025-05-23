using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Instagram.API.Models
{
    [Table("Posts")]
    public class Posts
    {
        [Key]
        public int Id { get; set; }

        public string? Description { get; set; }

        public byte[]? ImageBytes { get; set; }

        public string? PostType { get; set; }

        public DateTime PostDate { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }
        
    }
}
