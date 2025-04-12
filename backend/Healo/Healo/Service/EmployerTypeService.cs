using Healo.ErrorHandling;
using Healo.Models;
using Healo.Repository;
using Healo.Utils;

namespace Healo.Service;

public class EmployerTypeService
{
    private readonly IEmployerTypeRepository _repository;

    private static readonly Dictionary<JobGroup, EmployerType> JobGroupToEmployerTypeMap = new()
    {
        { JobGroup.Physician, EmployerType.Physician },
        { JobGroup.Pharmacy, EmployerType.Pharmacy },
        { JobGroup.Dentistry, EmployerType.Dentistry }
    };

    public EmployerTypeService(IEmployerTypeRepository repository)
    {
        _repository = repository;
    }

    public async Task<ErrorOr<IEnumerable<EmployerTypeModel>>> GetByJobGroupAsync(Guid jobGroupId)
    {
        try
        {
            var types = await _repository.GetByJobGroupAsync(jobGroupId);
            return ErrorOr<IEnumerable<Entry>>.FromIEnumerable(types); ;
        }
        catch (Exception ex)
        {
            return ErrorFactory.CreateError(ErrorType.Failure, "Failed to retrieve Employer Types", ex);
        }
    }

    public async Task<ErrorOr<IEnumerable<EmployerTypeModel>>> GetAllAsync()
    {
        try
        {
            var types = Enum.GetValues<EmployerType>()
                .Where(e => e != EmployerType.None && e != EmployerType.All)
                .Select(e => new EmployerTypeModel
                {
                    Id = Guid.NewGuid(),
                    Name = StringUtils.AddSpacesToCamelCase(e.ToString()),
                    Description = $"{e} Employer Type"
                });

            return ErrorOr<IEnumerable<EmployerTypeModel>>.FromIEnumerable(types);
        }
        catch (Exception ex)
        {
            return ErrorFactory.CreateError(ErrorType.Failure, "Failed to retrieve Employer Types", ex);
        }
    }

    public ErrorOr<IEnumerable<EmployerTypeModel>> GetByJobGroupEnum(JobGroup jobGroup)
    {
        try
        {
            var types = JobGroupToEmployerTypeMap.TryGetValue(jobGroup, out var employerType)
                ? ModelUtils.GetIndividualFlags(employerType)
                : Array.Empty<EmployerType>();

            var models = types.Select(e => new EmployerTypeModel
            {
                Id = Guid.NewGuid(),
                Name = StringUtils.AddSpacesToCamelCase(e.ToString()),
                Description = $"{e} Employer Type"
            });

            return ErrorOr<IEnumerable<EmployerTypeModel>>.FromIEnumerable(models);
        }
        catch (Exception ex)
        {
            return ErrorFactory.CreateError(ErrorType.Failure, "Failed to retrieve Employer Types", ex);
        }
    }
}
