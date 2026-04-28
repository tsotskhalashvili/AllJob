using AllJob.Application.Interfaces.Services.Candidate;
using AllJob.Application.Interfaces.Services.Job;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Security.Claims;

namespace AllJob.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AiController(
    ICvGenerationService cvGenerationService) : BaseController
{
    [HttpPost("generate-cv")]
    [Authorize(Roles = "Candidate")]
    [EnableRateLimiting("cv-generation")]
    public async Task<IActionResult> GenerateCv([FromQuery] string lang ="Ka")
    {
        var userId = Guid.Parse(
            User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var url = await cvGenerationService.GenerateCvAsync(userId,lang);
        return Ok(new { url });
    }
}