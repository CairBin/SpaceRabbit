using CommonInit;


var builder = WebApplication.CreateBuilder(args);

builder.ConfigureDbConfiguration();
builder.ConfigureExtraServices(new InitOptions
{
    EventBusQueueName = "LinkService.WebApi",
    LogFilePath = "e:/temp/LinkService.log"
});

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "LinkService.WebApi", Version = "v1" });
});

var app = builder.Build();

// Configure the Http request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LinkService.WebApi v1"));
}

app.UseDefault();
app.MapControllers();

app.Run();