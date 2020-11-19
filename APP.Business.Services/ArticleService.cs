using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using APP.Business.Services.Models;
using APP.DbAccess.Entities;
using APP.DbAccess.Repositories;
using APP.Framework.DynamicLinq;
using APP.Framework.IView;
using APP.Framework.Util;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace APP.Business.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IMapper _mapper;
        private readonly IArticleRepository _articleRepository;
        private readonly IChannelRepository _channleRepository;
        private readonly IFileRepository _fileRepository;

        public ArticleService(IMapper mapper, IArticleRepository articleRepository, IChannelRepository channleRepository, IFileRepository fileRepository)
        {
            _mapper = mapper;
            _articleRepository = articleRepository;
            _channleRepository = channleRepository;
            _fileRepository = fileRepository;
        }

        public async Task<string> AddArticleAsync(ArticleModel model)
        {
            model.Created = DateTime.Now;
            model.Updated = DateTime.Now;
            model.State = 1;
            var entity = _mapper.Map<Article>(model);
            entity.Id = Guid.NewGuid().ToString(10);
            await _articleRepository.AddAsync(entity);
            //包含的文件处理
            await _fileRepository.GetAll().Where(f => model.Files.Contains(f.Id)).ForEachAsync(f => f.OwnerId = entity.Id);

            await _articleRepository.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<bool> DelArticleAsync(string id)
        {
            var entity = await _articleRepository.GetByIdAsync(id);
            entity.State = 0;
            var rows = await _articleRepository.SaveChangesAsync();
            return rows > 0;
        }
        public async Task<bool> UpdateArticleAsync(ArticleModel model)
        {
            model.Updated = DateTime.Now;
            var entity = _mapper.Map<Article>(model);
            _articleRepository.Update(entity);
            _articleRepository.Entry(entity).Property(nameof(entity.Created)).IsModified = false;
            _articleRepository.Entry(entity).Property(nameof(entity.UserId)).IsModified = false;
            //包含的文件处理
            await _fileRepository.GetAll().Where(f => model.Files.Contains(f.Id)).ForEachAsync(f => f.OwnerId = entity.Id);

            var rows = await _articleRepository.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<ArticleModel> GetArticleAsync(string id)
        {
            var query = _articleRepository.GetAll().Where(a => a.Id.EndsWith(id));
            var model = await _mapper.ProjectTo<ArticleModel>(query).FirstOrDefaultAsync();
            return model;
        }
        public async Task<PageResult<List<ArticleListModel>>> GetArticlesAsync(SearchArticleModel model)
        {
            model.Sort = new List<Sort> { new Sort { Field = "Created", Desc = true } };
            Expression<Func<Article, bool>> ex = t => true;
            if (!string.IsNullOrWhiteSpace(model.Id))
            {
                ex = t => t.Id.Contains(model.Id);
            }
            if (!string.IsNullOrWhiteSpace(model.Title))
            {
                ex = ex.And(t => t.Title.Contains(model.Title));
            }
            if (!string.IsNullOrWhiteSpace(model.UserName))
            {
                ex = ex.And(t => t.User.UserName.Contains(model.UserName));
            }
            if (!string.IsNullOrWhiteSpace(model.CreatedDate))
            {
                DateTime.TryParse(model.CreatedDate, out var createdDate);
                ex = ex.And(t => t.Created.Date == createdDate);
            }
            if (!string.IsNullOrWhiteSpace(model.ChannelId))
            {
                ex = ex.And(t => t.ChannelId == model.ChannelId);
            }
            if (!string.IsNullOrWhiteSpace(model.MatchWord))
            {
                ex = ex.And(t => t.Title.Contains(model.MatchWord) || t.SubTitle.Contains(model.MatchWord) || t.Summary.Contains(model.MatchWord) || t.Content.Contains(model.MatchWord));
            }
            var users = await _articleRepository.GetAll().Include(i => i.Channel).Include(i => i.User).Include(i => i.Comments).Include(i => i.Files).Where(ex).ToDataSourceResultAsync(model);
            return new PageResult<List<ArticleListModel>>
            {
                Data = _mapper.Map<List<ArticleListModel>>(users.Data),
                Total = users.Total
            };
        }
        public async Task<string> AddChannelAsync(ChannelModel model)
        {
            var channel = _channleRepository.GetAll().Where(c => c.Title == model.Title.Trim() && c.ParentId == model.ParentId).FirstOrDefault();
            if (channel != null)
            {
                return channel.Id;
            }
            var entity = _mapper.Map<Channel>(model);
            entity.Id = Guid.NewGuid().ToString(10);
            entity.State = 1;
            await _channleRepository.AddAsync(entity);
            await _articleRepository.SaveChangesAsync();
            return entity.Id;
        }
        public async Task<List<Cascader>> GetChannelsToCascaderAsync(string channelId)
        {
            var channels = await _channleRepository.GetAll().Where(c => c.State == 1).ToListAsync();
            var result = new List<Cascader>();
            foreach (var c in channels.Where(c => c.ParentId == channelId))
            {
                var model = new Cascader
                {
                    Label = c.Title,
                    Value = c.Id
                };
                if (channels.Any(a => a.ParentId == c.Id))
                {
                    model.Children = new List<Cascader>();
                    model.Loading = false;
                }
                result.Add(model);
            }
            return result;
        }
    }
}
