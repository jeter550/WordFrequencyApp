namespace WordFrequency.API.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("health")]
[ApiExplorerSettings(IgnoreApi = true)]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Health()
    {
        return Ok(new { status = "healthy", timestamp = DateTime.UtcNow });
    }
}
