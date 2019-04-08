using System.Collections.Generic;
using System.Linq;
using APP.DbAccess.Repositories;
using APP.Business.Services.Models;
using AutoMapper;

namespace APP.Business.Services
{
    public class CommonService : ICommonService
    {
        private readonly IMapper _mapper;
        private readonly IMenuRepository _menuRepository;

        public CommonService(IMapper mapper, IMenuRepository menuRepository)
        {
            _mapper = mapper;
            _menuRepository = menuRepository;
        }

        public List<MenuModel> GetMenus()
        {
            var result = new List<MenuModel>();
            var menus = _menuRepository.GetAll().Where(m => m.State == 1);

            throw new System.NotImplementedException();
        }
    }
}
