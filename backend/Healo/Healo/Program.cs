using System.Text.Json.Serialization;
using Healo.Configuration;
using Healo.Configuration.Extensions;
using Healo.Models;
using Healo.Repository;
using Healo.Service;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(builder.Configuration["AllowedOrigins"] ?? "http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Add logging first
builder.Services.AddLogging();

// Add configuration sources
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

// Add services
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
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

// Use CORS
app.UseCors();

// Configure middleware
app.MapControllers();

await app.RunAsync();