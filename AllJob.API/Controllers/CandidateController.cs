using AllJob.Application.DTOs.Candidate;
using AllJob.Application.Interfaces.Services.Candidate;
using AllJob.Application.Interfaces.Services.Shared;
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
    ICandidateExperienceService experienceService,
     IFileUploadService fileUploadService,
    ICandidateEducationService educationService,
    IValidator<CreateCandidateProfileDto> createValidator,
    IValidator<UpdateCandidateProfileDto> updateValidator,
    IValidator<ExperienceDto> experienceValidator,
    IValidator<EducationDto> educationValidator) : BaseController
{
    private Guid UserId => Guid.Parse(
        User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

    // Public
    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPublicProfile(Guid id)
    {
        var result = await candidateService.GetPublicProfileAsync(id);
        return Ok(result);
    }

    // Profile
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

    [HttpPost("upload-photo")]
    public async Task<IActionResult> UploadPhoto(IFormFile file)
    {
        var url = await fileUploadService.UploadImageAsync(file);
        return Ok(new { url });
    }

    // Experience
    [HttpPost("experience")]
    public async Task<IActionResult> AddExperience(
        [FromBody] ExperienceDto dto)
    {
        await ValidateAsync(experienceValidator, dto);
        await experienceService.AddExperienceAsync(dto, UserId);
        return NoContent();
    }

    [HttpPut("experience/{id}")]
    public async Task<IActionResult> UpdateExperience(
        Guid id, [FromBody] ExperienceDto dto)
    {
        await ValidateAsync(experienceValidator, dto);
        await experienceService.UpdateExperienceAsync(id, dto, UserId);
        return NoContent();
    }

    [HttpDelete("experience/{id}")]
    public async Task<IActionResult> DeleteExperience(Guid id)
    {
        await experienceService.DeleteExperienceAsync(id, UserId);
        return NoContent();
    }

    // Education
    [HttpPost("education")]
    public async Task<IActionResult> AddEducation(
        [FromBody] EducationDto dto)
    {
        await ValidateAsync(educationValidator, dto);
        await educationService.AddEducationAsync(dto, UserId);
        return NoContent();
    }

    [HttpPut("education/{id}")]
    public async Task<IActionResult> UpdateEducation(
        Guid id, [FromBody] EducationDto dto)
    {
        await ValidateAsync(educationValidator, dto);
        await educationService.UpdateEducationAsync(id, dto, UserId);
        return NoContent();
    }

    [HttpPatch("skills")]
    public async Task<IActionResult> UpdateSkills(
    [FromBody] UpdateCandidateSkillsDto dto)
    {
        await candidateService.UpdateSkillsAsync(dto, UserId);
        return NoContent();
    }

    [HttpDelete("education/{id}")]
    public async Task<IActionResult> DeleteEducation(Guid id)
    {
        await educationService.DeleteEducationAsync(id, UserId);
        return NoContent();
    }
}