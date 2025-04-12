using Healo.Models;

namespace Healo.Repository;

public interface IEntryRepository
{
    Task<EntryEntity> CreateAsync(EntryEntity entry);
    Task<EntryEntity?> GetByIdAsync(Guid id);
    Task<IEnumerable<EntryEntity>> GetAllAsync();
    Task<EntryEntity?> UpdateAsync(Guid id, EntryEntity entry);
    Task<bool> DeleteAsync(Guid id);
}
