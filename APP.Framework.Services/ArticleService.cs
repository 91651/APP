using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using APP.DbAccess.Entities;
using APP.DbAccess.Repositories;
using APP.Framework.Services.Models;
using AutoMapper;
using IView.AspNetCore.DynamicLinq;
using Microsoft.EntityFrameworkCore;

namespace APP.Framework.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IMapper _mapper;
        private readonly IArticleRepository _articleRepository;
        private readonly IChannelRepository _channleRepository;

        public ArticleService(IMapper mapper, IArticleRepository articleRepository, IChannelRepository channleRepository)
        {
            _mapper = mapper;
            _articleRepository = articleRepository;
            _channleRepository = channleRepository;
        }

        public ResultModel<string> AddArticle(ArticleModel model)
        {
            model.Created = DateTime.Now;
            model.Updated = DateTime.Now;
            model.State = 1;
            var entity = _mapper.Map<Article>(model);
            entity.Id = Guid.NewGuid().ToString();
            _articleRepository.Add(entity);
            _articleRepository.SaveChanges();
            return new ResultModel<string>
            {
                Status = true,
                Data = entity.Id
            };
        }

        public ResultModel DelArticle(string id)
        {
            var entity = _articleRepository.GetById(id);
            entity.State = 0;
            var rows = _articleRepository.SaveChanges();
            return new ResultModel
            {
                Status = rows > 0
            };
        }
        public ResultModel UpdateArticle(ArticleModel model)
        {
            model.Updated = DateTime.Now;
            var entity = _mapper.Map<Article>(model);
            _articleRepository.Update(entity);
            _articleRepository.Entry(entity).Property(nameof(entity.Created)).IsModified = false;
            _articleRepository.Entry(entity).Property(nameof(entity.UserId)).IsModified = false;
            var rows = _articleRepository.SaveChanges();
            return new ResultModel
            {
                Status = rows > 0
            };
        }

        public ResultModel<ArticleModel> GetArticle(string id)
        {
            var entity = _articleRepository.GetAll().Include(i => i.Channel).AsNoTracking().FirstOrDefault(a => a.Id == id);
            var channels = new List<string>();
            var cid = entity.ChannelId;
            while (!string.IsNullOrWhiteSpace(cid))
            {
                channels.Add(cid);
                cid = _channleRepository.GetById(cid)?.ParentId;
            }
            var model = _mapper.Map<ArticleModel>(entity);
            channels.Reverse();
            model.ChannelId = channels.ToArray();
            return new ResultModel<ArticleModel>
            {
                Status = entity != null,
                Data = model
            };
        }
        public ResultModel<List<ArticleListModel>> GetArticles(SearchArticleModel model)
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
            var users = _articleRepository.GetAll().Include(i => i.Channel).Include(i => i.User).Where(ex).ToDataSourceResult(model);
            return new ResultModel<List<ArticleListModel>>
            {
                Data = _mapper.Map<List<ArticleListModel>>(users.Data),
                Total = users.Total
            };
        }
        public ResultModel<string> AddChannel(ChannelModel model)
        {
            var channel = _channleRepository.GetAll().Where(c => c.Title == model.Title.Trim() && c.ParentId == model.ParentId).FirstOrDefault();
            if (channel != null)
            {
                return new ResultModel<string>
                {
                    Status = true,
                    Data = channel.Id
                };
            }
            var entity = _mapper.Map<Channel>(model);
            entity.Id = Guid.NewGuid().ToString();
            entity.State = 1;
            _channleRepository.Add(entity);
            _articleRepository.SaveChanges();
            return new ResultModel<string>
            {
                Status = true,
                Data = entity.Id
            };
        }
        public List<Cascader> GetChannelsToCascader(string channelId)
        {
            var channels = _channleRepository.GetAll().Where(c => c.State == 1).ToList();
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
