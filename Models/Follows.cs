using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Instagram.API.Models
{
    [Table("Followers")]
    public class Follows
    {

        
            [Key, Column(Order = 0)]
            public int SeguidorId { get; set; }

            [Key, Column(Order = 1)]
            public int SeguidoId { get; set; }

            public DateTime DataFollow { get; set; } = DateTime.Now;

            public DateTime? DataUnFollow { get; set; }

           
            [ForeignKey("SeguidorId")]
            public virtual UserPosts? Seguidor { get; set; }

           
            [ForeignKey("SeguidoId")]
            public virtual UserPosts? Seguido { get; set; }
        }
}
