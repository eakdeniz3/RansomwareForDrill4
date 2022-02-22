using RFD.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFD.Bussiness.EntityFramework.Services.Abstact
{
    public interface IUnitOfWork : IDisposable 
    {
        IComponentService ComponentService { get; }
        IPhishingService PhishingService { get; }
        IInsiderService InsiderService { get; }
        public ITransectionService TransectionService { get; }
        public ISummaryService SummaryService { get; }

        public IUserService UserService { get; }

        Task<int> Complete();
    }
}
