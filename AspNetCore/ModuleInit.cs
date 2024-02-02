using Microsoft.Extensions.DependencyInjection;
using Commons;

namespace AspNetCore
{
    public class ModuleInit: IModuleInit
    {
        public void Initialize(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddDistributedMemoryCache();
            services.AddScoped<IMemoryCacheHelper, MemoryCacheHelper>();
            services.AddScoped<IDistributedCacheHelper, DistributedCacheHelper>();
        }
    }
}
