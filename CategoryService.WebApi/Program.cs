using CommonInit;
using CategoryService.Domain;
using CategoryService.Domain.Entity;
using CategoryService.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureDbConfiguration();
builder.ConfigureExtraServices(new InitOptions
{
    EventBusQueueName = "CategoryService.WebApi",
    LogFilePath = "e:/temp/CategoryService.log"
});

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "CategoryService.WebApi", Version = "v1" });
});

var app = builder.Build();

// Configure the Http request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CategoryService.WebApi v1"));
}

app.UseDefault();
app.MapControllers();

app.Run();