using RFD.Entities.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RFD.Entities.DTO
{
    public class Insider : EntityBase
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        [RegularExpression(@"^[-\w\s]+(?:,[\w\s]*)*$", ErrorMessage = "Giriş dizisi uygun formatta değil.")]
        public string Computers { get; set; }
      //  public bool IsTest { get; set; }
        public StatusType Status { get; set; }
        public virtual List<Transection> Transections { get; set; }
    }
}
