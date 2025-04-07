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
        services.AddOptions<DatabasesOptions>()
            .Bind(configuration.GetSection(DatabasesOptions.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        // MongoClient is thread-safe and meant to be used as a singleton
        services.AddSingleton(sp =>
        {
            var dbOptions = sp.GetRequiredService<IOptions<DatabasesOptions>>().Value;
            var mongoConfig = dbOptions.Databases["MongoDb"];
            return new MongoClient(mongoConfig.ConnectionString);
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