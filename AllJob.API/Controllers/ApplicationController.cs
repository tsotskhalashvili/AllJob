using AllJob.Application.DTOs.Application;
using AllJob.Application.Interfaces.Services.Applications;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AllJob.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ApplicationController(
    IApplicationService applicationService,
    IValidator<CreateApplicationDto> validator,
    IValidator<UpdateApplicationStatusDto> statusValidator) : BaseController
{
    private Guid UserId => Guid.Parse(
        User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

    [Authorize(Roles = "Candidate")]
    [HttpPost]
    public async Task<IActionResult> Apply([FromBody] CreateApplicationDto dto)
    {
        await ValidateAsync(validator, dto);
        var result = await applicationService.CreateAsync(dto, UserId);
        return Ok(result);
    }

    [Authorize(Roles = "Candidate")]
    [HttpGet("my")]
    public async Task<IActionResult> GetMyApplications()
    {
        var result = await applicationService.GetMyApplicationsAsync(UserId);
        return Ok(result);
    }

    [Authorize(Roles = "Employer")]
    [HttpGet("job/{jobId}")]
    public async Task<IActionResult> GetJobApplications(Guid jobId)
    {
        var result = await applicationService
            .GetJobApplicationsAsync(jobId, UserId);
        return Ok(result);
    }

    [Authorize(Roles = "Employer")]
    [HttpPatch("{id}/status")]
    public async Task<IActionResult> UpdateStatus(
     Guid id, [FromBody] UpdateApplicationStatusDto dto)
    {
        await ValidateAsync(statusValidator, dto);
        var result = await applicationService
            .UpdateStatusAsync(id, dto, UserId);
        return Ok(result);
    }
}