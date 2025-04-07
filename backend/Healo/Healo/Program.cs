using Healo.Configuration.Extensions;
using Healo.Models;
using Healo.Repository;
using Healo.Service;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Replace environment variables in configuration
config.GetSection("Databases:MongoDb:ConnectionString").Value =
    config.GetSection("Databases:MongoDb:ConnectionString").Value?
        .Replace("${MONGODB_PASSWORD}",
            Environment.GetEnvironmentVariable("MONGODB_PASSWORD") ??
            throw new InvalidOperationException("MONGODB_PASSWORD environment variable is not set"));

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