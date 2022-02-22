using RFD.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RFD.WebUI.Model
{
    public class InsiderModel : Insider
    {
        public InsiderModel()
        {

        }
        public InsiderModel(Insider insider)
        {
            if (insider is not null)
            {
                Id = insider.Id;
                Computers = insider.Computers;
                Transections = insider?.Transections;
                Title = insider?.Title;
                Status = insider.Status;
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
                    return Math.Ceiling((double)complateCount / totalCount * 100);
                }
                else
                {
                    return default(int);
                }
            }
        }

    }
}
