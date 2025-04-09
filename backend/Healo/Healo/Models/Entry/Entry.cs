using System.Collections.Concurrent;
using Riok.Mapperly.Abstractions;

namespace Healo.Models;

public class Entry
{
    public Guid Id { get; init; }
    public JobGroup JobGroup { get; init; }
    public Guid JobGroupId { get; init; }
    public EmployerType EmployerType { get; init; }
    public Guid EmployerTypeId { get; init; }
    public Level Level { get; init; }
    public Guid LevelId { get; init; }
    public double Salary { get; init; }
    public int WorkLifeBalanceScore { get; init; }
}

public class EntryRequest
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public JobGroup JobGroup { get; init; }
    public Guid JobGroupId { get; init; }
    public EmployerType EmployerType { get; init; }
    public Guid EmployerTypeId { get; init; }
    public Level Level { get; init; }
    public Guid LevelId { get; init; }
    public double Salary { get; init; }
    public int WorkLifeBalanceScore { get; init; }
}

public class EntryResponse
{
    public Guid Id { get; init; }
    public JobGroup JobGroup { get; init; }
    public Guid JobGroupId { get; init; }
    public EmployerType EmployerType { get; init; }
    public Guid EmployerTypeId { get; init; }
    public Level Level { get; init; }
    public Guid LevelId { get; init; }
    public double Salary { get; init; }
    public int WorkLifeBalanceScore { get; init; }
}

public class EntryEntity : Entity
{
    public JobGroup JobGroup { get; set; }
    public Guid JobGroupId { get; set; }
    public EmployerType EmployerType { get; set; }
    public Guid EmployerTypeId { get; set; }
    public Level Level { get; set; }
    public Guid LevelId { get; set; }
    public double Salary { get; set; }
    public int WorkLifeBalanceScore { get; set; }
}

[Mapper]
public partial class EntryMapper
{
    public partial EntryEntity ToEntity(Entry entry);
    public partial Entry ToModel(EntryEntity entity);
    public partial Entry ToModel(EntryRequest request);
    public partial EntryResponse ToResponse(Entry entry);

    public async Task<IEnumerable<Entry>> ToModels(IEnumerable<EntryEntity> entities)
    {
        var models = new ConcurrentQueue<Entry>();

        await Parallel.ForEachAsync(entities, async (entity, _) =>
        {
            var model = ToModel(entity);
            models.Enqueue(model);
        });

        return models;
    }

    public async Task<IEnumerable<EntryResponse>> ToResponses(IEnumerable<Entry> entries)
    {
        var responses = new ConcurrentQueue<EntryResponse>();

        await Parallel.ForEachAsync(entries, async (entry, _) =>
        {
            var response = ToResponse(entry);
            responses.Enqueue(response);
        });

        return responses;
    }
}