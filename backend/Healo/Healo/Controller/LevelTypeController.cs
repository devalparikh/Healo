using Healo.Models;
using Healo.Service;
using Microsoft.AspNetCore.Mvc;

namespace Healo.Controller;

public class LevelTypeController : CustomControllerBase
{
    private readonly LevelTypeService _service;
    private readonly LevelTypeMapper _mapper;

    public LevelTypeController(LevelTypeService service, LevelTypeMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();
        if (result.IsError) return Failure(result);

        var response = await _mapper.ToResponses(result.Value);
        return Ok(response);
    }

    [HttpGet("byJobGroup/{jobGroupId:guid}")]
    public async Task<IActionResult> GetByJobGroup(Guid jobGroupId)
    {
        var result = await _service.GetByJobGroupAsync(jobGroupId);
        if (result.IsError) return Failure(result);

        var response = await _mapper.ToResponses(result.Value);
        return Ok(response);
    }

    [HttpGet("byJobGroupType/{jobGroup}")]
    public IActionResult GetByJobGroupType(JobGroup jobGroup)
    {
        var result = _service.GetByJobGroupEnum(jobGroup);
        if (result.IsError) return Failure(result);
        return Ok(result.Value);
    }
}
