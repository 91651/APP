using APP.DbAccess.Entities;
using APP.DbAccess.Infrastructure;

namespace APP.DbAccess.Repositories
{
    public class ChannelRepository : Repository<Channel>, IChannelRepository
    {
        public ChannelRepository(AppDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
