using System.Collections.Generic;
using APP.Framework.Services.Models;

namespace APP.Framework.Services
{
    public interface IUserService
    {
        List<UserModel> GetUsers();
    }
}