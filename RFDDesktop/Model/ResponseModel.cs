using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RFDDesktop.Model
{
    [XmlRoot("ApiResponse")]
    public class ResponseModel<T>
    {

        [XmlElement("Data")]
        public T Data { get; set; }

        [XmlElement("IsSucceeded")]
        public bool IsSucceeded { get; set; }

        [XmlElement("Message")]
        public string Message { get; set; }
    }
}
