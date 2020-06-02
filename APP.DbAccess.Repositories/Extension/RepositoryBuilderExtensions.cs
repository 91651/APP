using Microsoft.Extensions.DependencyInjection;

namespace APP.DbAccess.Repositories
{
    /// <summary>
    /// 已由BC.Microsoft.DependencyInjection.Plus实现批量注入，此类代码目前无作用。
    /// </summary>
    public static class RepositoryBuilderExtensions
    {
        public static void AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
