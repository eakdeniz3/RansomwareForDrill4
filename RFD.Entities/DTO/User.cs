using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFD.Entities.DTO
{
    public class User:EntityBase
    {
        [Key]
        public int UserId { get; set; } 
        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }
}
