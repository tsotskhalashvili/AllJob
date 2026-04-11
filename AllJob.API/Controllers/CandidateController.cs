using AllJob.Application.DTOs.Candidate;
using AllJob.Application.Interfaces.Services.Candidate;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AllJob.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Candidate")]
public class CandidateController(
    ICandidateService candidateService,
    IValidator<CreateCandidateProfileDto> createValidator,
    IValidator<UpdateCandidateProfileDto> updateValidator) : BaseController
{
    private Guid UserId => Guid.Parse(
        User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile()
    {
        var result = await candidateService.GetProfileAsync(UserId);
        return Ok(result);
    }

    [HttpPost("profile")]
    public async Task<IActionResult> CreateProfile(
        [FromBody] CreateCandidateProfileDto dto)
    {
        await ValidateAsync(createValidator, dto);
        var result = await candidateService.CreateProfileAsync(dto, UserId);
        return Ok(result);
    }

    [HttpPatch("profile")]
    public async Task<IActionResult> PatchProfile(
      [FromBody] UpdateCandidateProfileDto dto)
    {
        await ValidateAsync(updateValidator, dto);
        var result = await candidateService.UpdateProfileAsync(dto, UserId);
        return Ok(result);
    }

    [HttpDelete("profile")]
    public async Task<IActionResult> DeleteProfile()
    {
        await candidateService.DeleteProfileAsync(UserId);
        return NoContent();
    }
}