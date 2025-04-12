using Healo.Models;
using Microsoft.EntityFrameworkCore;

namespace Healo.Repository;

public class EmployerTypeRepository : IEmployerTypeRepository
{
    private readonly EntryDbContext _dbContext;

    public EmployerTypeRepository(EntryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<EmployerTypeModel>> GetByJobGroupAsync(Guid jobGroupId)
    {
        return await _dbContext.EmployerTypes
            .Where(et => et.JobGroupId == jobGroupId)
            .Include(et => et.JobGroup)
            .ToListAsync();
    }

    public async Task<IEnumerable<EmployerTypeModel>> GetAllAsync()
    {
        return await _dbContext.EmployerTypes
            .Include(et => et.JobGroup)
            .ToListAsync();
    }
}