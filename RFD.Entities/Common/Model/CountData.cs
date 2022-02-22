using Microsoft.EntityFrameworkCore;
using RFD.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFD.Entities.Common.Model
{
    public class CountData
    {
        public int TransectionCount { get; set; }
        public int PhishingCount { get; set; }
        public int InsiderCount { get; set; }
        public int PhishingDidWorkCount { get; set; }
        public int InsiderDidWorkCount { get; set; }

        public int PhishingDidTrueClose { get; set; }
        public int InsiderDidTrueClose { get; set; }

        public int TotalDidTrueClose { get; set; }
        public int TotalDidWorkCount { get; set; }

        public static async Task<CountData> CalculateAsync(IQueryable<Summary> data) => new CountData
        {
            InsiderCount = await data.CountAsync(x => x.ApplicationType ==(int) Enum.ApplicationType.Insider),
            InsiderDidTrueClose = await data.CountAsync(x => x.DidTrueClose && x.ApplicationType == (int)Enum.ApplicationType.Insider),
            InsiderDidWorkCount = await data.CountAsync(x => x.DidItWork && x.ApplicationType == (int)Enum.ApplicationType.Insider),
            PhishingCount = await data.CountAsync(x => x.ApplicationType == (int)Enum.ApplicationType.Phising),
            PhishingDidTrueClose = await data.CountAsync(x => x.DidTrueClose && x.ApplicationType == (int)Enum.ApplicationType.Phising),
            PhishingDidWorkCount = await data.CountAsync(x => x.DidItWork && x.ApplicationType == (int)Enum.ApplicationType.Phising),
            TotalDidTrueClose = await data.CountAsync(x => x.DidTrueClose),
            TotalDidWorkCount = await data.CountAsync(x => x.DidItWork),
            TransectionCount = await data.CountAsync()
        };
    }
}
