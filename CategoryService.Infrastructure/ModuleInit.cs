using CategoryService.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Commons;
using CategoryService.Domain;

namespace CategoryService.Infrastructure;


class ModuleInit : IModuleInit
{
    public void Initialize(IServiceCollection services)
    {
        services.AddScoped<ICategoryRepo, CategoryRepo>();
        services.AddScoped<CategoryDomainSer>();
    }
}