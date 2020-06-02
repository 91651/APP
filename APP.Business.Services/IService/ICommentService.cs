using System.Collections.Generic;
using System.Threading.Tasks;
using APP.Business.Services.Models;

namespace APP.Business.Services
{
    public interface ICommentService
    {
        Task<bool> AddCommentAsync(CommentModel model);
        Task<List<CommentModel>> GetCommentsAsync();
    }
}