using Healo.Models;

namespace Healo.Repository;

public interface IJobGroupRepository
{
    Task<IEnumerable<JobGroupType>> GetAllAsync();
}
