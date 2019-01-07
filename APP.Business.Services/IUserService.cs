using System.Collections.Generic;
using APP.Business.Services.Models;

namespace APP.Business.Services
{
    public interface IUserService
    {
        List<UserModel> GetUsers();
    }
}