using System.Collections.Generic;

namespace APP.Business.Services.Models
{
    public class MenuModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ParentId { get; set; }
        public string Path { get; set; }
        public List<MenuModel> Children { get; set; } = new List<MenuModel>();
    }
}
