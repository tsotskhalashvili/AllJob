using AllJob.Application.DTOs.Company;
using AllJob.Application.Interfaces.Services.Company;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AllJob.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CompanyController(
    ICompanyService companyService,
    ICompanyReviewService reviewService,
    IValidator<CreateCompanyDto> createValidator,
    IValidator<UpdateCompanyDto> updateValidator,
    IValidator<CreateCompanyReviewDto> reviewValidator)
: BaseController
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCompanyById(Guid id)
    {
        var result = await companyService.GetCompanyByIdAsync(id);
        return Ok(result);
    }

    [Authorize(Roles = "Employer")]
    [HttpPost]
    public async Task<IActionResult> CreateCompany(
        [FromBody] CreateCompanyDto dto)
    {
        await ValidateAsync(createValidator, dto);
        var userId = Guid.Parse(User.FindFirst(
            ClaimTypes.NameIdentifier)!.Value);
        var result = await companyService
            .CreateCompanyAsync(dto, userId);
        return Ok(result);
    }

    [Authorize(Roles = "Employer")]
    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateCompany(
        Guid id, [FromBody] UpdateCompanyDto dto)
    {
        await ValidateAsync(updateValidator, dto);
        var userId = Guid.Parse(User.FindFirst(
            ClaimTypes.NameIdentifier)!.Value);
        await companyService.UpdateCompanyAsync(id, dto, userId);
        return NoContent();
    }

    [Authorize(Roles = "Employer")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCompany(Guid id)
    {
        var userId = Guid.Parse(User.FindFirst(
            ClaimTypes.NameIdentifier)!.Value);
        await companyService.DeleteCompanyAsync(id, userId);
        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetCompanies(
        [FromQuery] CompanyFilterDto filter)
    {
        var result = await companyService.GetCompaniesAsync(filter);
        return Ok(result);
    }

    [HttpGet("{id}/jobs")]
    public async Task<IActionResult> GetCompanyJobs(Guid id)
    {
        Guid? userId = User.Identity?.IsAuthenticated == true
            ? Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value)
            : null;
        var result = await companyService.GetCompanyJobsAsync(id, userId);
        return Ok(result);
    }

    [HttpGet("{id}/reviews")]
    public async Task<IActionResult> GetCompanyReviews(Guid id)
    {
        var result = await reviewService.GetCompanyReviewsAsync(id);
        return Ok(result);
    }

    [Authorize(Roles = "Candidate")]
    [HttpPost("{id}/reviews")]
    public async Task<IActionResult> CreateReview(
        Guid id, [FromBody] CreateCompanyReviewDto dto)
    {
        await ValidateAsync(reviewValidator, dto);
        var userId = Guid.Parse(
            User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        await reviewService.CreateReviewAsync(id, dto, userId);
        return NoContent();
    }
}