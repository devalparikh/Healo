using Healo.ErrorHandling;
using Healo.Models;
using Healo.Repository;
using Healo.Utils;

namespace Healo.Service;

public class LevelTypeService
{
    private readonly ILevelTypeRepository _repository;

    private static readonly Dictionary<JobGroup, EmployerType> JobGroupToEmployerTypeMap = new()
    {
        { JobGroup.Physician, EmployerType.Physician },
        { JobGroup.Pharmacy, EmployerType.Pharmacy },
        { JobGroup.Dentistry, EmployerType.Dentistry }
    };

    private static readonly Dictionary<JobGroup, Level> JobGroupToLevelMap = new()
    {
        { JobGroup.Physician, Level.PhysicianLevels },
        { JobGroup.Pharmacy, Level.PharmacyLevels },
        { JobGroup.Dentistry, Level.DentistryLevels }
    };

    public LevelTypeService(ILevelTypeRepository repository)
    {
        _repository = repository;
    }

    public async Task<ErrorOr<IEnumerable<LevelTypeModel>>> GetByJobGroupAsync(Guid jobGroupId)
    {
        try
        {
            var types = await _repository.GetByJobGroupAsync(jobGroupId);
            return ErrorOr<IEnumerable<Entry>>.FromIEnumerable(types);
        }
        catch (Exception ex)
        {
            return ErrorFactory.CreateError(ErrorType.Failure, "Failed to retrieve Job Groups", ex);
        }
    }

    public async Task<ErrorOr<IEnumerable<LevelTypeModel>>> GetAllAsync()
    {
        try
        {
            var types = Enum.GetValues<Level>()
                .Where(l => l != Level.None && l != Level.All)
                .Select(l => new LevelTypeModel
                {
                    Id = Guid.NewGuid(),
                    Name = StringUtils.AddSpacesToCamelCase(l.ToString()),
                    Description = $"{l} Level"
                });

            return ErrorOr<IEnumerable<LevelTypeModel>>.FromIEnumerable(types);
        }
        catch (Exception ex)
        {
            return ErrorFactory.CreateError(ErrorType.Failure, "Failed to retrieve Level Types", ex);
        }
    }

    public ErrorOr<IEnumerable<LevelTypeModel>> GetByJobGroupEnum(JobGroup jobGroup)
    {
        try
        {
            var types = JobGroupToLevelMap.TryGetValue(jobGroup, out var level)
                ? ModelUtils.GetIndividualFlags(level)
                : Array.Empty<Level>();

            var models = types.Select(l => new LevelTypeModel
            {
                Id = Guid.NewGuid(),
                Name = StringUtils.AddSpacesToCamelCase(l.ToString()),
                Description = $"{l} Level"
            });

            return ErrorOr<IEnumerable<LevelTypeModel>>.FromIEnumerable(models);
        }
        catch (Exception ex)
        {
            return ErrorFactory.CreateError(ErrorType.Failure, "Failed to retrieve Level Types", ex);
        }
    }
}
