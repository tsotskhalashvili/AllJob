using AllJob.Application.DTOs.Skill;
using AllJob.Application.Interfaces.Services.Shared;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllJob.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SkillController(
    ISkillService skillService,
    IValidator<CreateSkillDto> validator) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await skillService.GetAllAsync();
        return Ok(result);
    }

    [Authorize(Roles = "Admin,SuperAdmin")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSkillDto dto)
    {
        await ValidateAsync(validator, dto);
        var result = await skillService.CreateAsync(dto);
        return Ok(result);
    }
}