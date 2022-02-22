using Microsoft.EntityFrameworkCore;
using RFD.Bussiness.EntityFramework.Services.Abstact;
using RFD.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RFD.Bussiness.EntityFramework.Services.Concrete
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _db;
        public Repository(DbContext db)
        {
            _db = db;
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var tEntity = await _db.Set<TEntity>().AddAsync(entity);
            return tEntity.Entity;
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _db.Set<TEntity>().AddRangeAsync(entities);
        }

        public async Task<TEntity> FindAsync(object id)
        {
            return await _db.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _db.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(object id)
        {
            return await _db.Set<TEntity>().FindAsync(id);
        }

        public IQueryable<TEntity> Quereble()
        {
            return _db.Set<TEntity>();
        }



        public async Task RemoveAsync(TEntity entity)
        {
            _db.Set<TEntity>().Remove(entity);
            await Task.CompletedTask;
        }

        public async Task RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            _db.Set<TEntity>().RemoveRange(entities);
            await Task.CompletedTask;
        }

        public async Task<int> SaveAsync()
        {
            return await _db.SaveChangesAsync();
        }

        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _db.Set<TEntity>().SingleOrDefaultAsync(predicate);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            // _db.Update<TEntity>(entity);
            _db.Set<TEntity>().Attach(entity);
            _db.Entry(entity).State = EntityState.Modified;
            await Task.CompletedTask;
        }
    }
}
