using AllJob.Application.DTOs.Job;
using AllJob.Application.Interfaces.Services;
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
    IValidator<UpdateJobDto> updateValidator) 
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
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateJob(
       Guid id, UpdateJobDto dto)
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

}
