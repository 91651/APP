using System;
using System.Linq;
using System.Threading.Tasks;
using APP.DbAccess.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace APP.DbAccess.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        public AppDbContext _db { get; }
        public DbSet<TEntity> _dbSet { get; }

        public Repository(AppDbContext context)
        {
            _db = context;
            _dbSet = _db.Set<TEntity>();
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual async Task<TEntity> GetByIdAsync(string id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return _dbSet;
        }

        public virtual void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public virtual void Remove(string id)
        {
            _dbSet.Remove(_dbSet.Find(id));
        }

        public virtual async Task<int> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync();
        }
        public EntityEntry Entry(TEntity entity)
        {
            return _db.Entry(entity);
        }
        public void Dispose()
        {
            _db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
