using NeoSimpleLogger;

var builder = WebApplication.CreateBuilder();

builder.Logging.ClearProviders();
builder.Logging.AddProvider(new LoggerProvider());
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Настройка Swagger UI
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

app.MapControllers();

await app.RunAsync();
