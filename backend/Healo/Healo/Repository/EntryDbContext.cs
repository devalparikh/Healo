using Healo.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;

namespace Healo.Repository;

public class EntryDbContext : DbContext
{
    public DbSet<EntryEntity> Entries { get; init; }

    public static EntryDbContext Create(IMongoDatabase database) => 
        new(new DbContextOptionsBuilder<EntryDbContext>()
        .UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName)
        .Options);

    public EntryDbContext(DbContextOptions options) : base(options)
    {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<EntryEntity>().ToCollection("entries");
    }
}