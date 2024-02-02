using CommonInit;
var builder = WebApplication.CreateBuilder(args);

builder.ConfigureDbConfiguration();
builder.ConfigureExtraServices(new InitOptions
{
    LogFilePath = "e:/temp/OptionService.Admin.log",
    EventBusQueueName = "OptionService.Admin"
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen(c => {

    c.SwaggerDoc("v1", new() { Title = "OptionService.Admin.WebApi", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OptionService.Admin.WebApi v1"));
}

app.UseDefault();

app.MapControllers();

app.Run();
