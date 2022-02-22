using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFD.Entities.VM
{
    public class LoginVM
    {
        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        [StringLength(15,MinimumLength =6,ErrorMessage ="{0} alanı {2}-{1} aralığında olmalıdır.")]
        public string Password { get; set; }
    }
}
