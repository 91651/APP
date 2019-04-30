using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using APP.DbAccess.Repositories;
using APP.Business.Services.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace APP.Business.Services
{
    public class BlogService : IBlogService
    {
        private readonly IMapper _mapper;
        private readonly IArticleRepository _articleRepository;
        private readonly IChannelRepository _channleRepository;

        public BlogService(IMapper mapper, IArticleRepository articleRepository, IChannelRepository channleRepository)
        {
            _mapper = mapper;
            _articleRepository = articleRepository;
            _channleRepository = channleRepository;
        }
        public ArticleModel GetArticle(string id)
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
            return model;
        }
        public ArticleModel GetPrevArticle(string id, string channelId)
        {
            var article = _articleRepository.GetById(id);
            var entity = _articleRepository.GetAll().Where(a => (string.IsNullOrEmpty(channelId) || a.ChannelId == channelId) && a.Updated < article.Updated).OrderByDescending(o => o.Updated).FirstOrDefault();
            var model = _mapper.Map<ArticleModel>(entity);
            return model;
        }
        public ArticleModel GetNextArticle(string Id, string channelId)
        {
            var article = _articleRepository.GetById(Id);
            var entity = _articleRepository.GetAll().Where(a => (string.IsNullOrEmpty(channelId) || a.ChannelId == channelId) && a.Updated > article.Updated).OrderBy(o => o.Updated).FirstOrDefault();
            var model = _mapper.Map<ArticleModel>(entity);
            return model;
        }
        public ChannelModel GetChannel(string id)
        {
            var entity = _channleRepository.GetById(id);
            return _mapper.Map<ChannelModel>(entity);
        }
        public List<ChannelModel> GetChannels(string pid = null)
        {
            var channels = _channleRepository.GetAll().Where(c => c.State == 1 && c.ParentId == pid).ToList();
            var list = _mapper.Map<List<ChannelModel>>(channels);
            return list;
        }
        public int UpdateArticleViewed(string id)
        {
            var article = _articleRepository.GetById(id);
            article.Viewed++;
            _articleRepository.SaveChanges();
            return article.Viewed;
        }
        public List<ArticleModel> GetHotArticles()
        {
            var articles = _articleRepository.GetAll().Include(i => i.Channel).Include(i => i.User).Include(i => i.Files).OrderByDescending(a => a.Viewed).Take(10).ToList();
            return _mapper.Map<List<ArticleModel>>(articles);
        }
    }
}
