using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Instagram.API.Models
{
    public class Comments
    {
        [Key]
        public long Id { get; set; }  

        [Required]
        public string Comment { get; set; }

        public DateTime? DateComment { get; set; } = DateTime.Now;

        public DateTime? DateUpdated { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; }

        public int UserId { get; set; }
        public int PostId { get; set; }

        [ForeignKey("UserId")]
        [JsonIgnore]
        public virtual User? User { get; set; }   

        [ForeignKey("PostId")]
        [JsonIgnore]
        public virtual Posts? Post { get; set; }
    }
    }

