using Microsoft.AspNetCore.Builder;
using EventBus;

namespace CommonInit
{
    public static class AppBuilderExtend
    {
        public static IApplicationBuilder UseDefault(this IApplicationBuilder app)
        {
            app.UseEventBus();
            app.UseCors();
            app.UseForwardedHeaders();
            app.UseAuthentication();
            app.UseAuthorization();

            return app;
            
        }

    }
}