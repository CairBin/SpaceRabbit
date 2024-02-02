using Microsoft.Extensions.DependencyInjection;
using Commons;
using PageService.Domain;

namespace PageService.Infrastructure;

public class ModuleInit : IModuleInit
{
    public void Initialize(IServiceCollection services)
    {
        services.AddScoped<IPageRepo, PageRepo>();
        services.AddScoped<PageDomainSer>();
    }
}
