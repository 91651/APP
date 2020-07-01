using System.Collections.Generic;
using System.Linq;
using APP.DbAccess.Repositories;
using APP.Business.Services.Models;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using APP.DbAccess.Entities;
using System;

namespace APP.Business.Services
{
    public class CommentService : ICommentService
    {
        private readonly IMapper _mapper;
        private readonly ICommentRepository _commentRepository;

        public CommentService(IMapper mapper, ICommentRepository commentRepository)
        {
            _mapper = mapper;
            _commentRepository = commentRepository;
        }

        public async Task<bool> AddCommentAsync(CommentModel model)
        {
            var comment = _mapper.Map<Comment>(model);
            comment.Id = Guid.NewGuid().ToString();
            await _commentRepository.AddAsync(comment);
            return await _commentRepository.SaveChangesAsync() > 0;
        }

        public async Task<List<CommentModel>> GetCommentsAsync(string pid)
        {
            var comments = await _commentRepository.GetAll().ToListAsync();
            var list =  _mapper.Map<List<CommentModel>>(comments);
            list.ForEach(m => m.Comments = list.Where(c => c.ParentId == m.Id).ToList());
            var result = list.Where(t => t.ParentId == pid).ToList();
            return result;
        }
    }
}
