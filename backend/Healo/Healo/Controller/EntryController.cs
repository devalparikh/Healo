using Healo.Models;
using Healo.Service;
using Microsoft.AspNetCore.Mvc;

namespace Healo.Controller;

public class EntryController : CustomControllerBase
{
    private readonly EntryService _entryService;
    private readonly EntryMapper _entryMapper;

    public EntryController(EntryService entryService, EntryMapper entryMapper)
    {
        _entryService = entryService;
        _entryMapper = entryMapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateEntry([FromBody] EntryRequest request)
    {
        var entry = _entryMapper.ToModel(request);
        var result = await _entryService.CreateEntryAsync(entry);
        if (result.IsError) return Failure(result);

        var response = _entryMapper.ToResponse(result.Value);
        return Created($"Entry/{response.Id}", response);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetEntry(Guid id)
    {
        var result = await _entryService.GetEntryAsync(id);
        if (result.IsError) return Failure(result);
        return Ok(_entryMapper.ToResponse(result.Value));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllEntries()
    {
        var result = await _entryService.GetAllEntriesAsync();
        if (result.IsError) return Failure(result);

        var responses = await _entryMapper.ToResponses(result.Value);
        return Ok(responses);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateEntry(Guid id, [FromBody] EntryRequest request)
    {
        var entry = _entryMapper.ToModel(request);
        var result = await _entryService.UpdateEntryAsync(id, entry);
        if (result.IsError) return Failure(result);
        return Ok(_entryMapper.ToResponse(result.Value));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteEntry(Guid id)
    {
        var result = await _entryService.DeleteEntryAsync(id);
        if (result.IsError) return Failure(result);
        return NoContent();
    }
}
