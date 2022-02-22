using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RFDDesktop
{
    public class ApplicationData
    {


        public static ApplicationType ApplicationType
        {
            get
            {
                return ApplicationType.Insider;
            }
        }

        public static double WorkTimeAsMinute
        {
            get
            {
                return 0.5;
            }
        }

    }

    public enum ApplicationType
    {
        [XmlEnum(Name = "Phishing")]
        Phishing,
        [XmlEnum(Name = "Insider")]
        Insider
    }
}
