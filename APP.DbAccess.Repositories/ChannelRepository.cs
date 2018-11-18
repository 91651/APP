using APP.DbAccess.Entities;
using APP.DbAccess.Infrastructure;

namespace APP.DbAccess.Repositories
{
    public class ChannelRepository : Repository<Channel>, IChannelRepository
    {
        private readonly AppDbContext _db;

        public ChannelRepository(AppDbContext dbContext)
            : base(dbContext)
        {
            _db = dbContext;
        }
    }
}
