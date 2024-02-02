using CommentService.Infrastructure;
using CommentService.Infrastructure.Service;
using Microsoft.Extensions.DependencyInjection;
using Commons;
using CommentService.Domain;


namespace CommentService.Infrastructure;


class ModuleInit :IModuleInit
{
    public void Initialize(IServiceCollection services)
    {
        services.AddScoped<ICommentRepo, CommentRepo>();
        services.AddScoped<IRecaptchaValidator, RecaptchaValidator>();
        services.AddScoped<CommentDomainSer>();
        
    }
}