using Healo.ErrorHandling;
using Healo.Models;
using Healo.Repository;
using Healo.Utils;

namespace Healo.Service;

public class JobGroupService
{
    private readonly IJobGroupRepository _repository;

    public JobGroupService(IJobGroupRepository repository)
    {
        _repository = repository;
    }

    public async Task<ErrorOr<IEnumerable<JobGroupType>>> GetAllAsync()
    {
        try
        {
            var groups = Enum.GetValues<JobGroup>()
                .Select(g => new JobGroupType
                {
                    Id = Guid.NewGuid(),
                    Name = StringUtils.AddSpacesToCamelCase(g.ToString()),
                    Description = $"{g} Healthcare Group"
                });

            return ErrorOr<IEnumerable<JobGroupType>>.FromIEnumerable(groups);
        }
        catch (Exception ex)
        {
            return ErrorFactory.CreateError(ErrorType.Failure, "Failed to retrieve Job Groups", ex);
        }
    }
}
