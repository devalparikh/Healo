using Healo.ErrorHandling;
using Healo.Models;
using Healo.Repository;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Healo.Service;

public class EntryService
{
    private readonly IEntryRepository _entryRepository;
    private readonly EntryMapper _entryMapper;

    public EntryService(IEntryRepository entryRepository, EntryMapper entryMapper)
    {
        _entryRepository = entryRepository;
        _entryMapper = entryMapper;
    }

    

    public async Task<ErrorOr<Entry>> CreateEntryAsync(Entry entry)
    {
        try
        {
            if (entry == null)
                return ErrorFactory.CreateError(ErrorType.Validation, "Entry cannot be null");

            var entity = _entryMapper.ToEntity(entry);
            var result = await _entryRepository.CreateAsync(entity);
            return _entryMapper.ToModel(result);
        }
        catch (Exception ex)
        {
            return ErrorFactory.CreateError(ErrorType.Failure, "Failed to create entry", ex);
        }
    }

    public async Task<ErrorOr<Entry>> GetEntryAsync(Guid id)
    {
        try
        {
            if (id == Guid.Empty)
                return ErrorFactory.CreateError(ErrorType.Validation, "Invalid entry ID");

            var entity = await _entryRepository.GetByIdAsync(id);
            if (entity == null)
                return ErrorFactory.CreateError(ErrorType.NotFound, $"Entry with ID {id} not found");

            return _entryMapper.ToModel(entity);
        }
        catch (Exception ex)
        {
            return ErrorFactory.CreateError(ErrorType.Failure, "Failed to retrieve entry", ex);
        }
    }

    public async Task<ErrorOr<IEnumerable<Entry>>> GetAllEntriesAsync()
    {
        try
        {
            var entities = await _entryRepository.GetAllAsync();
            var entries = await _entryMapper.ToModels(entities);
            return ErrorOr<IEnumerable<Entry>>.FromIEnumerable(entries);
        }
        catch (Exception ex)
        {
            return ErrorFactory.CreateError(ErrorType.Failure, "Failed to retrieve entries", ex);
        }
    }

    public async Task<ErrorOr<Entry>> UpdateEntryAsync(Guid id, Entry entry)
    {
        try
        {
            if (id == Guid.Empty)
                return ErrorFactory.CreateError(ErrorType.Validation, "Invalid entry ID");

            if (entry == null)
                return ErrorFactory.CreateError(ErrorType.Validation, "Entry cannot be null");

            var entity = await _entryRepository.UpdateAsync(id, _entryMapper.ToEntity(entry));
            if (entity == null)
                return ErrorFactory.CreateError(ErrorType.NotFound, $"Entry with ID {id} not found");

            return _entryMapper.ToModel(entity);
        }
        catch (Exception ex)
        {
            return ErrorFactory.CreateError(ErrorType.Failure, "Failed to update entry", ex);
        }
    }

    public async Task<ErrorOr<bool>> DeleteEntryAsync(Guid id)
    {
        try
        {
            if (id == Guid.Empty)
                return ErrorFactory.CreateError(ErrorType.Validation, "Invalid entry ID");

            var result = await _entryRepository.DeleteAsync(id);
            if (!result)
                return ErrorFactory.CreateError(ErrorType.NotFound, $"Entry with ID {id} not found");

            return result;
        }
        catch (Exception ex)
        {
            return ErrorFactory.CreateError(ErrorType.Failure, "Failed to delete entry", ex);
        }
    }
}
