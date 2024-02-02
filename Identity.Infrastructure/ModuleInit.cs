using IdentityService.Domain;
using Microsoft.Extensions.DependencyInjection;
using Commons;


namespace IdentityService.Infrastructure
{
    internal class ModuleInit : IModuleInit
    {
        public void Initialize(IServiceCollection services)
        {
            //依赖注入
            services.AddScoped<IdentityDomainSer>();
            services.AddScoped<IIdentityRepo, IdRepo>();
        }
    }
}
