using System.ComponentModel.DataAnnotations.Schema;

namespace Instagram.API.Models
{
    public class Comments
    {
        public string Commets { get; set; }

        public int Id { get; set; }

        public DateTime DateComment { get; set; }

        public DateTime DateUpdated { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [ForeignKey("Post")]
        public int PostId { get; set; }
    }
}
