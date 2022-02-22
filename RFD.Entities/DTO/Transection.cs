using RFD.Entities.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RFD.Entities.DTO
{
    public class Transection : EntityBase
    {
        [Key]
        public int Id { get; set; }
        public string ComputerName { get; set; }

        public TransectionType TransectionType { get; set; }



        [ForeignKey("InsiderId")]
        public int? InsiderId { get; set; }
        public Insider Insider { get; set; }

    }


   
}

