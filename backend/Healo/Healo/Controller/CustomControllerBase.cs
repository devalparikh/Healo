using Healo.ErrorHandling;
using Microsoft.AspNetCore.Mvc;

namespace Healo.Controller;

[ApiController]
[Route("[controller]")]
public class CustomControllerBase : ControllerBase
{
    protected IActionResult Failure<T>(ErrorOr<T> error)
    {
        return StatusCode(error.StatusCode, new { Errors = error.Errors });
    }
}