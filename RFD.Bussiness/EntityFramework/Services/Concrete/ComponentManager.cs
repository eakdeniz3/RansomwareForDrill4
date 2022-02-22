using Microsoft.EntityFrameworkCore;
using RFD.Bussiness.EntityFramework.Services.Abstact;
using RFD.DataAccess.EntityFramework;
using RFD.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFD.Bussiness.EntityFramework.Services.Concrete
{
    public class ComponentManager : Repository<Component>, IComponentService
    {
        public RFDContext _context;

        public ComponentManager(RFDContext context) : base(context)
        {
            _context = context;
        }


        //public async Task<Component> GetAsync(string computerName)
        //{
        //    if (computerName is null)
        //    {
        //        throw new ArgumentNullException(nameof(computerName), "Veri boş olamaz");

        //    }
        //    var component = await _context.Components.FirstOrDefaultAsync(x => x.ComputerName.ToLower().Equals(computerName.ToLower()));

        //    if (component is null)
        //    {
        //        return null;
        //    }
        //    return component;
        //}

        //public async Task<(bool, Component)> IsExist(string computerName)
        //{
        //    if (computerName is null)
        //    {
        //        throw new ArgumentNullException("Veri boş olamaz");

        //    }


        //    var component = await GetAsync(computerName);

        //    if (component is null)
        //        return (false, new Component());
        //    return (true, component);
        //}
    }
}
