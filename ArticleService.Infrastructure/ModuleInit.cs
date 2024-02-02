using Microsoft.Extensions.DependencyInjection;
using Commons;
using ArticleService.Domain;
namespace ArticleService.Infrastructure
{
    class ModuleInit : IModuleInit
    {
        public void Initialize(IServiceCollection services)
        {
            services.AddScoped<IArticleRepo, ArticleRepo>();
            services.AddScoped<ArticleDomainSer>();
        }
    }
}
