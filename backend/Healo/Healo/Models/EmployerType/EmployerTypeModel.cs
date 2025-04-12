using Riok.Mapperly.Abstractions;

namespace Healo.Models;

public class EmployerTypeModel : Entity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid JobGroupId { get; set; }
    public JobGroupType JobGroup { get; set; }
}

public class EmployerTypeRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid JobGroupId { get; set; }
}

public class EmployerTypeResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid JobGroupId { get; set; }
}

[Mapper]
public partial class EmployerTypeMapper
{
    public partial EmployerTypeModel ToModel(EmployerTypeRequest request);
    public partial EmployerTypeResponse ToResponse(EmployerTypeModel employerType);

    public async Task<IEnumerable<EmployerTypeResponse>> ToResponses(IEnumerable<EmployerTypeModel> types)
    {
        return types.Select(t => ToResponse(t));
    }
}
