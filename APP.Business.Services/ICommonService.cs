using System.Collections.Generic;
using APP.Business.Services.Models;

namespace APP.Business.Services
{
    public interface ICommonService
    {
        List<MenuModel> GetMenus();
    }
}