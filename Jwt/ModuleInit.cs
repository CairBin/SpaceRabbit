using Microsoft.Extensions.DependencyInjection;
using Commons;

namespace Jwt
{
    public class ModuleInit:IModuleInit
    {
        public void Initialize(IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
        }
    }
}
