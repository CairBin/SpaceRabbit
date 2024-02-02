using Microsoft.Extensions.DependencyInjection;

namespace Commons
{
    public interface IModuleInit
    {
        public void Initialize(IServiceCollection services);
    }
}
