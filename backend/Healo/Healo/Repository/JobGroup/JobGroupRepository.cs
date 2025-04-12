using Healo.Models;
using Microsoft.EntityFrameworkCore;

namespace Healo.Repository;

public class JobGroupRepository : IJobGroupRepository
{
    private readonly EntryDbContext _dbContext;

    public JobGroupRepository(EntryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<JobGroupType>> GetAllAsync()
    {
        return await _dbContext.JobGroups.ToListAsync();
    }
}
