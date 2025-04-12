using Healo.Models;

namespace Healo.Repository;

public interface ILevelTypeRepository
{
    Task<IEnumerable<LevelTypeModel>> GetAllAsync();
    Task<IEnumerable<LevelTypeModel>> GetByJobGroupAsync(Guid jobGroupId);
}