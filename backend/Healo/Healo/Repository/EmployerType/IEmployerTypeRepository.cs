using Healo.Models;

namespace Healo.Repository;

public interface IEmployerTypeRepository
{
    Task<IEnumerable<EmployerTypeModel>> GetAllAsync();
    Task<IEnumerable<EmployerTypeModel>> GetByJobGroupAsync(Guid jobGroupId);
}