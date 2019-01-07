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
                .ForMember(m => m.ChannelId, opt => opt.Ignore())
                .ForMember(m => m.ChannelName, opt => opt.MapFrom(s => s.Channel.Title))
                .ReverseMap()
                .ForMember(m => m.Channel, opt => opt.Ignore())
                .ForMember(m => m.ChannelId, opt => opt.MapFrom(s => s.ChannelId.Last()));
            CreateMap<Article, ArticleListModel>()
                .ForMember(m => m.ChannelName, opt => opt.MapFrom(s => s.Channel.Title))
                .ForMember(m => m.UserName, opt => opt.MapFrom(s => s.User.UserName))
                .ForMember(m => m.File, opt => opt.MapFrom(s => s.Files.FirstOrDefault()));
            CreateMap<User, UserModel>();
            CreateMap<Channel, ChannelModel>();
            CreateMap<File, FileModel>();
        }

        //以下代码在本项目中暂未用到
        public static void RegisterMappings()
        {
            //获取所有IProfile实现类
            var allType =
            Assembly
               .GetEntryAssembly()//获取默认程序集
               .GetReferencedAssemblies()//获取所有引用程序集
               .Select(Assembly.Load)
               .SelectMany(y => y.DefinedTypes)
               .Where(type => typeof(IProfile).GetTypeInfo().IsAssignableFrom(type.AsType()));

            foreach (var typeInfo in allType)
            {
                var type = typeInfo.AsType();
                if (type.Equals(typeof(IProfile)))
                {
                    //注册映射
                    Mapper.Initialize(y =>
                    {
                        y.AddProfiles(type); // Initialise each Profile classe
                    });
                }
            }
        }
    }
}
