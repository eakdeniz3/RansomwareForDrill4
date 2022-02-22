using Microsoft.EntityFrameworkCore;
using RFD.Bussiness.EntityFramework.Services.Abstact;
using RFD.DataAccess.EntityFramework;
using RFD.Entities.Common.FilterModel;
using RFD.Entities.Common.Model;
using RFD.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RFD.Infrastructer.EF.Helpers;


namespace RFD.Bussiness.EntityFramework.Services.Concrete
{
    public class InsiderManager : Repository<Insider>, IInsiderService
    {

        public RFDContext _context;

        public InsiderManager(RFDContext context):base(context)
        {
            _context = context;
        }

        public async Task<PagedList<Insider>> Get(InsiderParamerters paramerters)
        {
            IQueryable<Insider> phishings = _context.Insider.Include(x => x.Transections).OrderBy(paramerters.OrderBy);
            if (paramerters?.Search is not null)
            {
                phishings = phishings.Where(x => x.Title.ToLower().Contains(paramerters.Search.ToLower()));
            }

            return await PagedList<Insider>
               .ToPagedListAsync(phishings, paramerters.PageNumber, paramerters.PageSize);
        }
    }
}
