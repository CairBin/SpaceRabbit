using OptionService.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Commons;
using OptionService.Domain;
namespace OptionService.Infrastructure
{
    public class ModuleInit : IModuleInit
    {
        public void Initialize(IServiceCollection services)
        {
            services.AddScoped<IOptionRepo, OptionRepo>();
            services.AddScoped<OptionDomainSer>();
        }
    }
}
