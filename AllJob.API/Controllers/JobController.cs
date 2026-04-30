using AllJob.Application.DTOs.Job;
using AllJob.Application.Interfaces.Services.Job;
using AllJob.Application.Services.Job;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AllJob.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JobController(
    IJobService jobService,
    IValidator<CreateJobDto> createValidator,
    IValidator<UpdateJobDto> updateValidator,
    IJobMatchingService jobMatchingService) 
    : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetJobs([FromQuery] JobFilterDto filter)
    {
        var result = await jobService.GetJobsAsync(filter);
        return Ok(result);

    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetJobId(Guid id)
    {
        var result = await jobService.GetJobByIdAsync(id);
        return Ok(result);

    }

    [Authorize(Roles = "Employer")]
    [HttpPost]
    public async Task<IActionResult> CreateJob([FromBody] CreateJobDto dto)
    {
        await ValidateAsync(createValidator, dto);

        var userId = Guid.Parse(User.FindFirst(
            ClaimTypes.NameIdentifier)!.Value);

        var result = await jobService.CreateJobAsync(dto, userId);
        return Ok(result);
    }

    [Authorize(Roles = "Employer")]
    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateJob(
       Guid id, [FromBody] UpdateJobDto dto)
    {
        await ValidateAsync(updateValidator, dto);

        var userId = Guid.Parse(User.FindFirst(
            ClaimTypes.NameIdentifier)!.Value);

        await jobService.UpdateJobAsync(id, dto, userId);
        return NoContent();
    }

    [Authorize(Roles = "Employer")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteJob(Guid id) 
    {
        var userId = Guid.Parse(User.FindFirst(
            ClaimTypes.NameIdentifier)!.Value);

        await jobService.DeleteJobAsync(id, userId);
        return NoContent();
    }

    [Authorize(Roles = "Employer")]
    [HttpGet("{id}/applications/count")]
    public async Task<IActionResult> GetApplicationsCount(Guid id)
    {
        var userId = Guid.Parse(
            User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var result = await jobService.GetApplicationsCountAsync(id, userId);
        return Ok(result);
    }

    [HttpGet("recommended")]
    [Authorize(Roles = "Candidate")]
    public async Task<IActionResult> GetRecommended()
    {
        var userId = Guid.Parse(
            User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var result = await jobMatchingService.GetRecommendedJobsAsync(userId);
        return Ok(result);
    }

    [HttpGet("{id}/match")]
    [Authorize(Roles = "Candidate")]
    public async Task<IActionResult> GetMatchScore(Guid id)
    {
        var userId = Guid.Parse(
            User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var score = await jobMatchingService.GetJobMatchScoreAsync(userId, id);
        return Ok(new { score });
    }

    [Authorize(Roles = "Employer")]
    [HttpPatch("{id}/publish")]
    public async Task<IActionResult> PublishJob(Guid id)
    {
        var userId = Guid.Parse(
            User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        await jobService.PublishJobAsync(id, userId);
        return NoContent();
    }

}
