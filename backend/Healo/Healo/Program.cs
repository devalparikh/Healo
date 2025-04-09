using Healo.Configuration;
using Healo.Configuration.Extensions;
using Healo.Models;
using Healo.Repository;
using Healo.Service;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add logging first
builder.Services.AddLogging();

// Add configuration sources
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Database setup
builder.Services.AddMongoDbConfiguration(config);
builder.Services.AddHostedService<DatabaseInitializer>();

// Services
builder.Services.AddScoped<EntryService>();

// Repositories
builder.Services.AddScoped<IEntryRepository, EntryRepository>();

// Adapters
builder.Services.AddSingleton<EntryMapper>();

var app = builder.Build();

// Configure middleware
app.MapControllers();

await app.RunAsync();