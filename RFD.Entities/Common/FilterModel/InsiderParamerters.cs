using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFD.Entities.Common.FilterModel
{
    public class InsiderParamerters : QueryStringParameters
    {
        public InsiderParamerters()
        {

            OrderBy = "Id DESC";
        }
        public string Search { get; set; }

    }
}
