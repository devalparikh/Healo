using Healo.Models;
using Healo.Service;
using Microsoft.AspNetCore.Mvc;

namespace Healo.Controller;

public class EmployerTypeController : CustomControllerBase
{
    private readonly EmployerTypeService _service;
    private readonly EmployerTypeMapper _mapper;

    public EmployerTypeController(EmployerTypeService service, EmployerTypeMapper mapper)
    {
        _service = service;
        _mapper = mapper;
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

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();
        if (result.IsError) return Failure(result);

        var response = await _mapper.ToResponses(result.Value);
        return Ok(response);
    }
}
