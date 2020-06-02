using APP.DbAccess.Entities;
using APP.DbAccess.Infrastructure;

namespace APP.DbAccess.Repositories
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(AppDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
