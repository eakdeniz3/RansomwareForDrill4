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
    public class SummaryManager : Repository<Summary>, ISummaryService
    {
        public RFDContext _context;

        public SummaryManager(RFDContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PagedList<Summary>> Get(SummaryParamerters paramerters)
        {
            IQueryable<Summary> computersRunningApp = _context.Summaries.OrderBy(paramerters.OrderBy);
            if (paramerters?.Search is not null)
            {
                computersRunningApp = computersRunningApp.Where(x => (x.ComputerName+x.Email+x.UserName).ToLower().Contains(paramerters.Search.ToLower()));
            }

            return await PagedList<Summary>
               .ToPagedListAsync(computersRunningApp, paramerters.PageNumber, paramerters.PageSize);
        }

        public Task<CountData> GetCountDataAsync() =>  CountData.CalculateAsync(_context.Summaries);
       
    }
}
