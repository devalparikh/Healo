using Healo.Configuration.Options;
using Healo.Repository;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Healo.Configuration.Extensions;

public static class DatabaseExtension
{
    public static IServiceCollection AddMongoDbConfiguration(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = Environment.GetEnvironmentVariable("MONGODB_URI");
        if (connectionString == null)
        {
            Console.WriteLine("You must set your 'MONGODB_URI' environment variable. To learn how to set it, see https://www.mongodb.com/docs/drivers/csharp/current/quick-start/#set-your-connection-string");
            Environment.Exit(0);
        }

        // Get configuration section
        var dbSection = configuration.GetSection(DatabasesOptions.SectionName).Get<DatabasesOptions>();
        if (dbSection?.Databases == null)
        {
            throw new InvalidOperationException("Database configuration is missing");
        }

        // Get MongoDB password from environment
        // var mongoDbPassword = Environment.GetEnvironmentVariable("MONGODB_PASSWORD") ??
        //     throw new InvalidOperationException("MONGODB_PASSWORD environment variable is not set");

        // Replace password placeholder in connection string
        // var mongoConfig = dbSection.Databases["MongoDb"];
        // mongoConfig.ConnectionString = mongoConfig.ConnectionString.Replace("__MONGODB_PASSWORD__", mongoDbPassword);
        // Console.WriteLine("ConnectionString" + mongoConfig.ConnectionString);
        services.AddOptions<DatabasesOptions>()
            .Configure(options =>
            {
                options.Databases = dbSection.Databases;
            })
            .ValidateDataAnnotations()
            .ValidateOnStart();

        // MongoClient is thread-safe and meant to be used as a singleton
        services.AddSingleton(sp =>
        {
            // var options = sp.GetRequiredService<IOptions<DatabasesOptions>>().Value;
            // var config = options.Databases["MongoDb"];
            return new MongoClient(connectionString);
        });

        // IMongoDatabase is thread-safe but scoped for better resource management
        services.AddScoped(sp =>
        {
            var client = sp.GetRequiredService<MongoClient>();
            var dbOptions = sp.GetRequiredService<IOptions<DatabasesOptions>>().Value;
            var mongoConfig = dbOptions.Databases["MongoDb"];
            return client.GetDatabase(mongoConfig.DatabaseName);
        });

        // DbContext should be scoped to ensure proper unit of work pattern
        services.AddScoped(sp =>
        {
            var database = sp.GetRequiredService<IMongoDatabase>();
            return EntryDbContext.Create(database);
        });

        return services;
    }
}