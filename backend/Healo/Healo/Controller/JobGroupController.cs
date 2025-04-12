using Healo.Models;
using Healo.Service;
using Microsoft.AspNetCore.Mvc;

namespace Healo.Controller;

public class JobGroupController : CustomControllerBase
{
    private readonly JobGroupService _service;
    private readonly JobGroupTypeMapper _mapper;

    public JobGroupController(JobGroupService service, JobGroupTypeMapper mapper)
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
}
