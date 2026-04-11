using AllJob.Application.DTOs.JobCategory;
using AllJob.Application.Interfaces.Services.Shared;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllJob.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JobCategoryController(
    IJobCategoryService jobCategoryService,
    IValidator<CreateJobCategoryDto> validator) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await jobCategoryService.GetAllAsync();
        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateJobCategoryDto dto)
    {
        await ValidateAsync(validator, dto);
        var result = await jobCategoryService.CreateAsync(dto);
        return Ok(result);
    }
}