using Healo.Configuration.Extensions;
using Healo.Models;
using Healo.Repository;
using Healo.Service;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Add configuration sources
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

// Database
builder.Services.AddMongoDbConfiguration(config);

// Services
builder.Services.AddScoped<EntryService>();

// Repositories
builder.Services.AddScoped<IEntryRepository, EntryRepository>();

// Adapters
builder.Services.AddSingleton<EntryMapper>();

var app = builder.Build();
app.MapControllers();
app.Run();