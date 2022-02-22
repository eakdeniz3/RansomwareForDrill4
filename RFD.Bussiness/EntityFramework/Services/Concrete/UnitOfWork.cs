using RFD.Bussiness.EntityFramework.Services.Abstact;
using RFD.DataAccess.EntityFramework;
using RFD.Entities.DTO;
using System;
using System.Threading.Tasks;

namespace RFD.Bussiness.EntityFramework.Services.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        public bool isDisposed = false;
       public readonly RFDContext _dbContext;
        public UnitOfWork(RFDContext context)
        {
            _dbContext = context;
        }

        public IComponentService ComponentService => new ComponentManager(_dbContext);


        public IPhishingService PhishingService => new PhishingManager(_dbContext);

     

        public IInsiderService InsiderService => new InsiderManager(_dbContext);




        public ITransectionService TransectionService => new TransectionManager(_dbContext);
        public ISummaryService SummaryService => new SummaryManager(_dbContext);

        public IUserService UserService =>  new UserManager(_dbContext);

        public async Task<int> Complete()
        {
            int result = await _dbContext.SaveChangesAsync();
            return result;
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
            isDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
