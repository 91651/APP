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
        void Add(TEntity entity);
        Task<TEntity> GetByIdAsync(string id);
        IQueryable<TEntity> GetAll();
        void Update(TEntity entity);
        void Remove(string id);
        int SaveChanges();
        EntityEntry Entry(TEntity entity);
    }
}