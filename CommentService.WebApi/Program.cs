using CommonInit;
using CommentService.Domain;
using CommentService.Domain.Entity;
using CommentService.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CommentService.Infrastructure.Options;
using reCAPTCHA.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureDbConfiguration();
builder.ConfigureExtraServices(new InitOptions
{
    EventBusQueueName = "CommentService.WebApi",
    LogFilePath = "e:/temp/CommentService.log"
});

//谷歌验证码
builder.Services.AddRecaptcha(options =>
{
    var config = builder.Configuration.GetSection("Recaptcha").Get<RecaptchaOptions>();

    options.Site = options.Site;
    options.SiteKey = options.SiteKey;
    options.SecretKey = options.SecretKey;
});

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "CommentService.WebApi", Version = "v1" });
});


var app = builder.Build();

// Configure the Http request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CommentService.WebApi v1"));
}

app.UseDefault();
app.MapControllers();

app.Run();