using System.Collections.Generic;
using System.Linq;
using APP.DbAccess.Repositories;
using APP.Business.Services.Models;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using APP.DbAccess.Entities;
using System;
using Microsoft.Extensions.Caching.Memory;

namespace APP.Business.Services
{
    public class CommentService : ICommentService
    {
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly ICommentRepository _commentRepository;

        public CommentService(IMapper mapper, IMemoryCache memoryCache, ICommentRepository commentRepository)
        {
            _mapper = mapper;
            _memoryCache = memoryCache;
            _commentRepository = commentRepository;
        }

        public async Task<bool> AddCommentAsync(CommentModel model, string captchaCode)
        {
            var validation = _memoryCache.Get<bool>(captchaCode);
            if(!validation)
            {
                return false;
            }
            if (!string.IsNullOrWhiteSpace(model.ParentId))
            {
               var existComment = _commentRepository.GetAll().Any(c => c.Id == model.ParentId);
                if (!existComment)
                {
                    return false;
                }
            }
            var comment = _mapper.Map<Comment>(model);
            comment.Id = Guid.NewGuid().ToString();
            comment.Created = DateTime.UtcNow;
            await _commentRepository.AddAsync(comment);
            return await _commentRepository.SaveChangesAsync() > 0;
        }

        public async Task<List<CommentModel>> GetCommentsAsync(string ownerId, string pid)
        {
            var comments = await _commentRepository.GetAll().Where(c => c.OwnerId.EndsWith(ownerId)).OrderByDescending(c => c.Created).ToListAsync();
            var list =  _mapper.Map<List<CommentModel>>(comments);
            list.ForEach(m => m.Comments = list.Where(c => c.ParentId == m.Id).ToList());
            var result = list.Where(t => t.ParentId == pid).ToList();
            return result;
        }
    }
}
