using System.Linq;
using System.Reflection;
using APP.DbAccess.Entities;
using APP.Business.Services.Models;
using AutoMapper;

namespace APP.Business.Services.AutoMapper
{
    public class Mappings : Profile, IProfile
    {
        public Mappings()
        {
            CreateMap<Article, ArticleModel>()
                .ForMember(m => m.ChannelName, opt => opt.MapFrom(s => s.Channel.Title))
                .ForMember(m => m.Files, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(m => m.Channel, opt => opt.Ignore())
                .ForMember(m => m.ChannelId, opt => opt.MapFrom(s => s.ChannelId.Last()));
            CreateMap<Article, ArticleListModel>()
                .ForMember(m => m.ChannelName, opt => opt.MapFrom(s => s.Channel.Title))
                .ForMember(m => m.UserName, opt => opt.MapFrom(s => s.User.UserName))
                .ForMember(m => m.File, opt => opt.MapFrom(s => s.Files.FirstOrDefault()));
            CreateMap<Channel, ChannelModel>();
            CreateMap<Comment, CommentModel>().ReverseMap();
            CreateMap<File, FileModel>();
            CreateMap<User, UserModel>();
        }
    }
}
