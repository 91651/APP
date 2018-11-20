using System.Linq;

namespace APP.DbAccess.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity obj);
        TEntity GetById(string id);
        IQueryable<TEntity> GetAll();
        void Update(TEntity obj);
        void Remove(string id);
        int SaveChanges();

    }
}