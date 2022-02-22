using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFDDesktop.Model
{
    public class Computer
    {
   
        public string ComputerName { get; set; }

        public string UserName { get; set; }

        public bool IsWorkPhishing { get; set; }

        public bool IsWorkInsider { get; set; }

        public bool IsWorkTVDO { get; set; }
    }
}
