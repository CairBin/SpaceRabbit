using Microsoft.Extensions.DependencyInjection;
using Commons;
using LinkService.Domain;

namespace LinkService.Infrastructure
{
    public class ModuleInit : IModuleInit
    {
        public void Initialize(IServiceCollection services)
        {
            services.AddScoped<ILinkRepo, LinkRepo>();

        }
    }
}
