using System;
using System.ComponentModel.DataAnnotations;

namespace RFD.Entities.DTO
{
    public class Component:EntityBase
    {


        [Key]
        public int ComponentId { get; set; }



        public string ComputerName { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }

        public string IpAddress { get; set; }

        public bool IsRead { get; set; }

        public bool IsClickLink { get; set; }

        public bool IsWorkPhishingg { get; set; }

        public bool IsWorkInsider { get; set; }

        public bool IsWorkTVDO { get; set; }

    }
}
