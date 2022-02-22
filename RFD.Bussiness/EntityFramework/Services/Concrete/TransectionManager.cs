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
    public class TransectionManager : Repository<Transection>, ITransectionService
    {
        public RFDContext _context;

        public TransectionManager(RFDContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PagedList<Transection>> Get(TransectionParamerters paramerters)
        {
            IQueryable<Transection> phishings = _context.Transections.OrderBy(paramerters.OrderBy);
            if (paramerters?.Search is not null)
            {
                phishings = phishings.Where(x => (x.ComputerName).ToLower().Contains(paramerters.Search.ToLower()));
            }

            return await PagedList<Transection>
               .ToPagedListAsync(phishings, paramerters.PageNumber, paramerters.PageSize);
        }

       // public Task<CountData> GetCountDataAsync() =>  CountData.CalculateAsync(_context.Transections);
       
    }
}
