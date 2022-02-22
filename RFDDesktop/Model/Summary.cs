using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFDDesktop.Model
{
    public class Summary 
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string ComputerName { get; set; }
        public string UserName { get; set; }
        public bool DidTrueClose { get; set; }
        public int ApplicationType { get; set; }
    }
}
