using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFD.Entities.Common.FilterModel
{
    public class PhishingParamerters : QueryStringParameters
    {
        public PhishingParamerters()
        {

            OrderBy = "Id DESC";
        }
        public string Search { get; set; }

    }
}
