using RFD.Entities.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RFD.Entities.DTO
{
    public class Summary : EntityBase
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string ComputerName { get; set; }
        public string UserName { get; set; }
        public bool DidItWork { get; set; }
        public bool DidTrueClose { get; set; }
        public int ApplicationType { get; set; }
        public TransectionType TransectionType { get; set; }

      


      

    }


   
}

