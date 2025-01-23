using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using Microsoft.AspNetCore.Mvc;

namespace Instagram.API.Model
{
    
    [Table ("Users")]
    public class User 
    {
        [Key]
        public  int Id { get; set; }

        [Required]
        [StringLength(11)]
        public string cpf { get; set; }

        [Required]
        [StringLength(255)]
        public string UserName { get; set; }


        [Required]
        [StringLength(255)]
        public string Passaword { get; set; }

        [StringLength(255)]
        public string email { get; set;  }


        public string DataNascimento { get; set; }

        public DataSetDateTime CreatedAt { get; set; }

        public   DateTime ? UpatetedAt { get; set; } = DateTime.Now;

    }
}
