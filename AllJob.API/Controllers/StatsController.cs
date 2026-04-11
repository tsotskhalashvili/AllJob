using AllJob.Application.Interfaces.Services.Shared;
using Microsoft.AspNetCore.Mvc;

namespace AllJob.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StatsController(
    IStatsService statsService) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetPublicStats()
    {
        var result = await statsService.GetPublicStatsAsync();
        return Ok(result);
    }
}