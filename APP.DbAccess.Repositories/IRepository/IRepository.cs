using System.Linq;
using System.Threading.Tasks;
using APP.DbAccess.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace APP.DbAccess.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        AppDbContext _db { get; }
        DbSet<TEntity> _dbSet { get; }
        Task AddAsync(TEntity entity);
        Task<TEntity> GetByIdAsync(string id);
        IQueryable<TEntity> GetAll();
        void Update(TEntity entity);
        void Remove(string id);
        Task<int> SaveChangesAsync();
        EntityEntry Entry(TEntity entity);
    }
}