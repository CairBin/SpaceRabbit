using Microsoft.Extensions.DependencyInjection;
using Commons;
using TagService.Domain;

namespace TagService.Infrastructure;

public class ModuleInit : IModuleInit
{
    public void Initialize(IServiceCollection services)
    {
        services.AddScoped<ITagRepo, TagRepo>();
        services.AddScoped<TagDomainSer>();
    }
}
