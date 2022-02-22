using RFD.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RFD.WebUI.Model
{
    public class PhishingModel : Phishing
    {
        public PhishingModel()
        {
                
        }
        public PhishingModel(Phishing phishing)
        {
            if (phishing is not null)
            {
                Id = phishing.Id;
                Transections = phishing?.Transections;
                Title = phishing?.Title;
               
            }
        }
        public bool IsChecked { get; set; }
        public bool IsSelect { get; set; }

        public double Progress
        {
            get
            {
                if (Transections.Any())
                {
                    int complateCount = Transections.Count(x => x.TransectionType == Entities.Enum.TransectionType.Success);
                    int totalCount = Transections.Count();
                    return Math.Ceiling( (double)complateCount / totalCount * 100);
                }
                else
                {
                    return default(int);
                }
            }
        }

    }
}
