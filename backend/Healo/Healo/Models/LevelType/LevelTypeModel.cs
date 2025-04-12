using Riok.Mapperly.Abstractions;

namespace Healo.Models;

public class LevelTypeModel : Entity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid JobGroupId { get; set; }
    public JobGroupType JobGroup { get; set; }
}

public class LevelTypeRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid JobGroupId { get; set; }
}

public class LevelTypeResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid JobGroupId { get; set; }
}

[Mapper]
public partial class LevelTypeMapper
{
    public partial LevelTypeModel ToModel(LevelTypeRequest request);
    public partial LevelTypeResponse ToResponse(LevelTypeModel levelType);

    public async Task<IEnumerable<LevelTypeResponse>> ToResponses(IEnumerable<LevelTypeModel> types)
    {
        return types.Select(t => ToResponse(t));
    }
}
