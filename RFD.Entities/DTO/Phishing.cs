using RFD.Entities.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RFD.Entities.DTO
{
    public class Phishing : EntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Başlık")]
        [Required(ErrorMessage ="{0} alanı zorunludur.")]
        public string Title { get; set; }
        [Display(Name = "Uygulama tipi")]
        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        public ApplicationType ApplicationType { get; set; }
        public StatusType Status { get; set; }
       
        public virtual List<Transection> Transections { get; set; }


    }
}
