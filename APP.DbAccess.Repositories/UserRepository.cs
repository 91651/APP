using APP.DbAccess.Entities;
using APP.DbAccess.Infrastructure;

namespace APP.DbAccess.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext dbContext)
            : base(dbContext)
        {
        }
        /*
         _dbContext.Channels.Where(w => w.Id != "").Update(u => new Channel { State = 3 });
            _dbContext.SaveChanges();

            _dbContext.Database.ExecuteSqlCommand($"update {nameof(Channel)} set State=@state", new[]
             {
                 new SqlParameter("state", 3)
             });
         */
    }
}
