using System;
using IView.AspNetCore.DynamicLinq;

namespace APP.Framework.Services.Models
{
    public class SearchArticleModel: Query
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string UserName { get; set; }
        public string CreatedDate { get; set; }
    }
}
