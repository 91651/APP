using System.Linq;
using System.Reflection;
using APP.DbAccess.Entities;
using APP.Framework.Services.Models;
using AutoMapper;

namespace APP.Framework.Services.AutoMapper
{
    public class Mappings : Profile, IProfile
    {
        public Mappings()
        {
            CreateMap<User, UserModel>();
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
