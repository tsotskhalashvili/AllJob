using AllJob.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AllJob.API.Controllers;

[Route("api/saved-jobs")]
[ApiController]
[Authorize(Roles = "Candidate")]
public class SavedJobController(
    ISavedJobService savedJobService) : BaseController
{
    private Guid UserId => Guid.Parse(
        User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

    [HttpGet]
    public async Task<IActionResult> GetMySavedJobs()
    {
        var result = await savedJobService.GetMySavedJobsAsync(UserId);
        return Ok(result);
    }

    [HttpPost("{jobId}")]
    public async Task<IActionResult> SaveJob(Guid jobId)
    {
        await savedJobService.SaveJobAsync(jobId, UserId);
        return Ok();
    }

    [HttpDelete("{jobId}")]
    public async Task<IActionResult> UnsaveJob(Guid jobId)
    {
        await savedJobService.UnsaveJobAsync(jobId, UserId);
        return NoContent();
    }
}