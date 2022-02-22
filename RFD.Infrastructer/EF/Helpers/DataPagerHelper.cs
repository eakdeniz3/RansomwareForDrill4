using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RFD.Infrastructer.EF.Helpers
{
    public static class DataPagerHelper
    {
        //public static async Task<PaginatedList<TModel>> PaginateAsync<TModel>(
        //   this IQueryable<TModel> query,
        //   int page,
        //   int limit
        // )
        //   where TModel : class
        //{

        //    var paged = new pahr<TModel>();
        //    page = (page < 0) ? 1 : page;

        //    paged.CurrentPage = page;
        //    paged.PageSize = limit;

        //    var totalItemsCountTask = query.CountAsync();

        //    var startRow = (page - 1) * limit;
        //    paged.Items = await query
        //               .Skip(startRow)
        //               .Take(limit)
        //               .ToListAsync();

        //    paged.TotalItems = await totalItemsCountTask;
        //    paged.TotalPages = (int)Math.Ceiling(paged.TotalItems / (double)limit);

        //    return paged;
        //}
    }
}
