using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APP.DbAccess.Entities;

namespace APP.DbAccess.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
    }
}
