using NeoSimpleLogger;

var builder = WebApplication.CreateBuilder();

builder.Logging.ClearProviders();
builder.Logging.AddProvider(new LoggerProvider());
builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();
app.Map("/", () => "Use http://host:5000/api/command/execute\nwith POST request\nExample request \n{\n  \"command\": \"ls -la\",\n  \"userId\": 32132,\n  \"chatId\": 3231\n}");

await app.RunAsync();
