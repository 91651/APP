using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using APP.DbAccess.Repositories;
using APP.Business.Services.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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
        public async Task<ArticleModel> GetArticleAsync(string id)
        {
            var entity = await _articleRepository.GetAll().Include(i => i.Channel).AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
            var channels = new List<string>();
            var cid = entity.ChannelId;
            while (!string.IsNullOrWhiteSpace(cid))
            {
                channels.Add(cid);
                cid = (await _channleRepository.GetByIdAsync(cid))?.ParentId;
            }
            var model = _mapper.Map<ArticleModel>(entity);
            //channels.Reverse();
            //model.ChannelId = channels.ToArray();
            return model;
        }
        public async Task<ArticleModel> GetPrevArticleAsync(string id, string channelId)
        {
            var article = await _articleRepository.GetByIdAsync(id);
            var entity = await _articleRepository.GetAll().Where(a => (string.IsNullOrEmpty(channelId) || a.ChannelId == channelId) && a.Updated < article.Updated).OrderByDescending(o => o.Updated).FirstOrDefaultAsync();
            var model = _mapper.Map<ArticleModel>(entity);
            return model;
        }
        public async Task<ArticleModel> GetNextArticleAsync(string Id, string channelId)
        {
            var article = await _articleRepository.GetByIdAsync(Id);
            var entity = await _articleRepository.GetAll().Where(a => (string.IsNullOrEmpty(channelId) || a.ChannelId == channelId) && a.Updated > article.Updated).OrderBy(o => o.Updated).FirstOrDefaultAsync();
            var model = _mapper.Map<ArticleModel>(entity);
            return model;
        }
        public async Task<ChannelModel> GetChannelAsync(string id)
        {
            var entity = await _channleRepository.GetByIdAsync(id);
            return _mapper.Map<ChannelModel>(entity);
        }
        public async Task<List<ChannelModel>> GetChannelsAsync(string pid = null)
        {
            var channels = await _channleRepository.GetAll().Where(c => c.State == 1 && c.ParentId == pid).ToListAsync();
            var list = _mapper.Map<List<ChannelModel>>(channels);
            return list;
        }
        public async Task<int> UpdateArticleViewedAsync(string id)
        {
            var article = await _articleRepository.GetByIdAsync(id);
            article.Viewed++;
            await _articleRepository.SaveChangesAsync();
            return article.Viewed;
        }
    }
}
