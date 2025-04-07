using Healo.Models;
using Microsoft.EntityFrameworkCore;

namespace Healo.Repository;

public class EntryRepository : IEntryRepository
{
    private readonly EntryDbContext _dbContext;

    public EntryRepository(EntryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<EntryEntity> CreateAsync(EntryEntity entry)
    {
        await _dbContext.Entries.AddAsync(entry);
        await _dbContext.SaveChangesAsync();
        return entry;
    }

    public async Task<EntryEntity?> GetByIdAsync(Guid id)
        => await _dbContext.Entries.FindAsync(id);

    public async Task<IEnumerable<EntryEntity>> GetAllAsync()
        => await _dbContext.Entries.ToListAsync();

    public async Task<EntryEntity?> UpdateAsync(Guid id, EntryEntity entry)
    {
        var existing = await GetByIdAsync(id);
        if (existing == null) return null;

        entry.Id = existing.Id;
        _dbContext.Entry(existing).CurrentValues.SetValues(entry);
        await _dbContext.SaveChangesAsync();
        return entry;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entry = await GetByIdAsync(id);
        if (entry == null) return false;

        _dbContext.Entries.Remove(entry);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}
