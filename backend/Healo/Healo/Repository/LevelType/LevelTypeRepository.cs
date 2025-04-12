using Healo.Models;
using Microsoft.EntityFrameworkCore;

namespace Healo.Repository;

public class LevelTypeRepository : ILevelTypeRepository
{
    private readonly EntryDbContext _dbContext;

    public LevelTypeRepository(EntryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<LevelTypeModel>> GetByJobGroupAsync(Guid jobGroupId)
    {
        return await _dbContext.LevelTypes
            .Where(lt => lt.JobGroupId == jobGroupId)
            .Include(lt => lt.JobGroup)
            .ToListAsync();
    }

    public async Task<IEnumerable<LevelTypeModel>> GetAllAsync()
    {
        return await _dbContext.LevelTypes
            .Include(lt => lt.JobGroup)
            .ToListAsync();
    }
}