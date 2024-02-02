using CommonInit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using reCAPTCHA.AspNetCore;
using CommentService.Infrastructure.Options;
using CommentService.Domain;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureDbConfiguration();
builder.ConfigureExtraServices(new InitOptions
{
    LogFilePath = "e:/temp/CommentService.Admin.log",
    EventBusQueueName = "CommentService.Admin"
});
// Add services to the container.
builder.Services.AddControllers();

//谷歌验证码
builder.Services.AddRecaptcha(options =>
{
    var config = builder.Configuration.GetSection("Recaptcha").Get<RecaptchaOptions>();

    options.Site = options.Site;
    options.SiteKey = options.SiteKey;
    options.SecretKey = options.SecretKey;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen(c => {

    c.SwaggerDoc("v1", new() { Title = "CommentService.Admin.WebApi", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CommentService.Admin.WebApi v1"));
}

app.UseDefault();

app.MapControllers();

app.Run();
