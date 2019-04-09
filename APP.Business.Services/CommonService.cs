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
            var menus = _mapper.Map<List<MenuModel>>(_menuRepository.GetAll().Where(m => m.State == 1));
            foreach (var menu in menus)
            {
                var model = menus.FirstOrDefault(f => f.Id == menu.ParentId);
                model?.Children.Add(menu);
            }
            return menus.Where(m => string.IsNullOrWhiteSpace(m.ParentId)).ToList();
        }
    }
}
