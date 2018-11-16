using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
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

        public ArticleService(IMapper mapper, IArticleRepository articleRepository)
        {
            _mapper = mapper;
            _articleRepository = articleRepository;
        }

        public string AddArticle(ArticleModel model)
        {
           var entity = _mapper.Map<Article>(model);
            entity.Id = Guid.NewGuid().ToString();
            _articleRepository.Add(entity);
            _articleRepository.SaveChanges();
            return entity.Id;
        }

        public List<ArticleListModel> GetArticles(SearchArticleModel model)
        {
            List<Sort> sorts = new List<Sort>();
            //sorts.Add(new Sort { Field = "Title", Desc = true });
            //sorts.Add(new Sort { Field = "Created"});

            var users = _articleRepository.GetAll().ToDataSourceResult(model);
            return _mapper.Map<List<ArticleListModel>>(users.Data);
        }
    }
}
