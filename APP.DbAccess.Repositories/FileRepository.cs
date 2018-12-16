using APP.DbAccess.Entities;
using APP.DbAccess.Infrastructure;

namespace APP.DbAccess.Repositories
{
    public class FileRepository : Repository<File>, IFileRepository
    {
        public FileRepository(AppDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
