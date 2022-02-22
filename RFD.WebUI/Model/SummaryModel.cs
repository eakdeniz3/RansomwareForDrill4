using RFD.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RFD.WebUI.Model
{
    public class SummaryModel : Summary
    {
        public SummaryModel(Summary summary)
        {
            if (summary is not null)
            {

            

            Id = summary.Id;
            Email = summary.Email;
            ComputerName = summary.ComputerName;
            UserName = summary.UserName;
            DidItWork = summary.DidItWork;
            DidTrueClose = summary.DidTrueClose;
            ApplicationType = summary.ApplicationType;
            TransectionType = summary.TransectionType;}
        }

        public bool IsChecked { get; set; }
        public bool IsSelect { get; set; }



    }
}
