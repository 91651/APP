using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace APP.Business.Services.Models
{
    public class CommentModel
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        public string OwnerId { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public List<CommentModel> Comments { get; set; }
    }
}
