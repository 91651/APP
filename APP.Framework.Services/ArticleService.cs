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

        public string AddArticle(ArticleModel model)
        {
           var entity = _mapper.Map<Article>(model);
            entity.Id = Guid.NewGuid().ToString();
            _articleRepository.Add(entity);
            _articleRepository.SaveChanges();
            return entity.Id;
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
            if (model.CreatedDate != null)
            {
                var createdDate = model.CreatedDate?.Date;
                ex = ex.And(t => t.Created.Date == createdDate);
            }
            var users = _articleRepository.GetAll().Where(ex).ToDataSourceResult(model);
            return new ResultModel<List<ArticleListModel>>
            {
                Data = _mapper.Map<List<ArticleListModel>>(users.Data),
                Total = users.Total
            };
        }
        public string AddChannel(ChannelModel model)
        {
            var channel = _channleRepository.GetAll().Where(c => c.Title == model.Title.Trim() && c.ParentId == model.ParentId).FirstOrDefault();
            if (channel != null)
            {
                return channel.Id;
            }
            var entity = _mapper.Map<Channel>(model);
            entity.Id = Guid.NewGuid().ToString();
            entity.State = 1;
            _channleRepository.Add(entity);
            _articleRepository.SaveChanges();
            return entity.Id;
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
                if(channels.Any(a => a.ParentId == c.Id))
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
