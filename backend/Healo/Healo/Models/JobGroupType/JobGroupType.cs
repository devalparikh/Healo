using Riok.Mapperly.Abstractions;

namespace Healo.Models;

public class JobGroupType : Entity
{
    public string Name { get; set; }
    public string Description { get; set; }
}

public class JobGroupTypeRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
}

public class JobGroupTypeResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}

[Mapper]
public partial class JobGroupTypeMapper
{
    public partial JobGroupType ToModel(JobGroupTypeRequest request);
    public partial JobGroupTypeResponse ToResponse(JobGroupType jobGroup);

    public async Task<IEnumerable<JobGroupTypeResponse>> ToResponses(IEnumerable<JobGroupType> types)
    {
        return types.Select(t => ToResponse(t));
    }
}
