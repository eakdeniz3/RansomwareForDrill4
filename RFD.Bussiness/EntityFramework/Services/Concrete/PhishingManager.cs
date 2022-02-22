using Microsoft.EntityFrameworkCore;
using RFD.Bussiness.EntityFramework.Services.Abstact;
using RFD.DataAccess.EntityFramework;
using RFD.Entities.Common.FilterModel;
using RFD.Entities.Common.Model;
using RFD.Entities.DTO;
using System;
using RFD.Infrastructer.EF.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace RFD.Bussiness.EntityFramework.Services.Concrete
{
    public class PhishingManager : Repository<Phishing>, IPhishingService
    {

        public RFDContext _context;

        public PhishingManager(RFDContext context) : base(context)
        {
            _context = context;
        }

        //public async Task<bool> IsSendAsync(int id)
        //{
        //    var model = await _context.Phishing.FindAsync(id);
        //    if (model is null)
        //    {
        //        throw new ArgumentNullException("", "Veri bulunamadı");
        //    }

        //    return false;
        //}

        //public async Task<PagedList<Phishing>> Get(PhishingParamerters paramerters)
        //{
        //    IQueryable<Phishing> phishings = _context.Phishing.Include(x => x.Transections).OrderBy(paramerters.OrderBy);
        //    if (paramerters?.Search is not null)
        //    {
        //        phishings=phishings.Where(x => x.Title.ToLower().Contains(paramerters.Search.ToLower()));
        //    }

        //    return await PagedList<Phishing>
        //       .ToPagedListAsync(phishings, paramerters.PageNumber, paramerters.PageSize);
        //}

    }
}
