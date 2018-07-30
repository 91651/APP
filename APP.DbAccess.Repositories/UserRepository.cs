using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APP.DbAccess.Entities;
using APP.DbAccess.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace APP.DbAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private AppDbContext _dbContext;

        public UserRepository(AppDbContext context)
        {
            _dbContext = context;
        }
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var users = await _dbContext.Users.ToListAsync();
            return users;
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
