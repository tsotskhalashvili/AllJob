using AllJob.Application.Interfaces.Services.Admin;
using AllJob.Domain.Enums.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AllJob.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin,SuperAdmin")]
public class AdminController(
    ICandidateManagerService candidateManagerService,
    IEmployerManagerService employerManagerService,
    IContentModeratorService contentModeratorService,
    IFullAccessService fullAccessService) : BaseController
{
    private Guid UserId => Guid.Parse(
        User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

    private bool IsSuperAdmin()
        => User.IsInRole("SuperAdmin");

    private AdminRole? GetAdminRole()
    {
        var value = User.FindFirst("AdminRole")?.Value;
        return Enum.TryParse<AdminRole>(value, out var role) ? role : null;
    }

    private bool HasAccess(params AdminRole[] allowedRoles)
        => IsSuperAdmin() ||
           (GetAdminRole().HasValue &&
            allowedRoles.Contains(GetAdminRole()!.Value));


    #region CandidateManager
    [HttpGet("candidates")]
    public async Task<IActionResult> GetAllCandidates()
    {
        if (!HasAccess(AdminRole.CandidateManager, AdminRole.FullAccess))
            return Forbid();

        var result = await candidateManagerService.GetAllUsersAsync();
        return Ok(result);
    }

    [HttpPatch("candidates/{id}/deactivate")]
    public async Task<IActionResult> DeactivateCandidate(Guid id)
    {
        if (!HasAccess(AdminRole.CandidateManager, AdminRole.FullAccess))
            return Forbid();

        await candidateManagerService.DeactivateUserAsync(id);
        return NoContent();
    }

    [HttpDelete("candidates/{id}")]
    public async Task<IActionResult> DeleteCandidate(Guid id)
    {
        if (!HasAccess(AdminRole.CandidateManager, AdminRole.FullAccess))
            return Forbid();

        await candidateManagerService.DeleteUserAsync(id);
        return NoContent();
    }
    #endregion

    #region EmployerManager 
    [HttpGet("employers")]
    public async Task<IActionResult> GetAllEmployers()
    {
        if (!HasAccess(AdminRole.EmployerManager, AdminRole.FullAccess))
            return Forbid();

        var result = await employerManagerService.GetAllEmployersAsync();
        return Ok(result);
    }

    [HttpPatch("employers/{id}/deactivate")]
    public async Task<IActionResult> DeactivateEmployer(Guid id)
    {
        if (!HasAccess(AdminRole.EmployerManager, AdminRole.FullAccess))
            return Forbid();

        await employerManagerService.DeactivateEmployerAsync(id);
        return NoContent();
    }

    [HttpDelete("employers/{id}")]
    public async Task<IActionResult> DeleteEmployer(Guid id)
    {
        if (!HasAccess(AdminRole.EmployerManager, AdminRole.FullAccess))
            return Forbid();

        await employerManagerService.DeleteEmployerAsync(id);
        return NoContent();
    }

    [HttpGet("companies")]
    public async Task<IActionResult> GetAllCompanies()
    {
        if (!HasAccess(AdminRole.EmployerManager, AdminRole.FullAccess))
            return Forbid();

        var result = await employerManagerService.GetAllCompaniesAsync();
        return Ok(result);
    }

    [HttpPatch("companies/{id}/verify")]
    public async Task<IActionResult> VerifyCompany(Guid id)
    {
        if (!HasAccess(AdminRole.EmployerManager, AdminRole.FullAccess))
            return Forbid();

        await employerManagerService.VerifyCompanyAsync(id);
        return NoContent();
    }

    [HttpPatch("companies/{id}/reject")]
    public async Task<IActionResult> RejectCompany(Guid id)
    {
        if (!HasAccess(AdminRole.EmployerManager, AdminRole.FullAccess))
            return Forbid();

        await employerManagerService.RejectCompanyAsync(id);
        return NoContent();
    }
    #endregion

    #region ContentModerator

    [HttpGet("reviews/pending")]
    public async Task<IActionResult> GetPendingReviews()
    {
        if (!HasAccess(AdminRole.ContentModerator, AdminRole.FullAccess))
            return Forbid();

        var result = await contentModeratorService.GetPendingReviewsAsync();
        return Ok(result);
    }

    [HttpPatch("reviews/{id}/approve")]
    public async Task<IActionResult> ApproveReview(Guid id)
    {
        if (!HasAccess(AdminRole.ContentModerator, AdminRole.FullAccess))
            return Forbid();

        await contentModeratorService.ApproveReviewAsync(id);
        return NoContent();
    }

    #endregion

    #region FullAccess

    [HttpGet("stats")]
    public async Task<IActionResult> GetStats()
    {
        if (!HasAccess(AdminRole.FullAccess))
            return Forbid();

        var result = await fullAccessService.GetAdminStatsAsync();
        return Ok(result);
    }

    #endregion
}