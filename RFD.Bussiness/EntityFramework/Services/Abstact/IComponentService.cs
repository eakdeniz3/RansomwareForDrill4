using RFD.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFD.Bussiness.EntityFramework.Services.Abstact
{
    public interface IComponentService : IRepository<Component>
    {
        //Task<(bool result, Component component)> IsExist(string computerName);
        //Task<Component> GetAsync(string computerName);
    }
}
