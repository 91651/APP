using System;
using System.Collections.Generic;
using System.Linq;
using APP.DbAccess.Entities;
using APP.DbAccess.Repositories;
using APP.Framework.Services.Models;
using AutoMapper;

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

        public List<ArticleListModel> GetArticles()
        {
            var users = _articleRepository.GetAll().Take(5).ToList();
            return _mapper.Map<List<ArticleListModel>>(users);
        }
    }
}
