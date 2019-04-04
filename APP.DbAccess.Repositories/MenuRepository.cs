using APP.DbAccess.Entities;
using APP.DbAccess.Infrastructure;

namespace APP.DbAccess.Repositories
{
    public class MenuRepository : Repository<Menu>, IMenuRepository
    {
        public MenuRepository(AppDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
