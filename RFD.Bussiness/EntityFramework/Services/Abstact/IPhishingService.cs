using RFD.Entities.Common.FilterModel;
using RFD.Entities.Common.Model;
using RFD.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFD.Bussiness.EntityFramework.Services.Abstact
{
    public interface IPhishingService : IRepository<Phishing>
    {
        //Task<bool> IsSendAsync(int id);
        //Task<PagedList<Phishing>> Get(PhishingParamerters paramerters);
    }
}
