using System.Collections.Generic;

namespace APP.Business.Services.Models
{
    public class CommentModel
    {
        public string NickName { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
        public List<CommentModel> Comments { get; set; }
    }
}
