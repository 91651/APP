using APP.DbAccess.Entities;
using APP.DbAccess.Infrastructure;

namespace APP.DbAccess.Repositories
{
    public class ArticleRepository : Repository<Article>, IArticleRepository
    {
        private readonly AppDbContext _db;

        public ArticleRepository(AppDbContext dbContext)
            : base(dbContext)
        {
            _db = dbContext;
        }
    }
}
