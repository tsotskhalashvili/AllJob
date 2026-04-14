using AllJob.Application.DTOs.Management;
using AllJob.Application.Interfaces.Services.Management;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AllJob.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "SuperAdmin")]
public class ManagementController(
    IManagementService managementService,
    IValidator<InviteAdminDto> inviteValidator,
    IValidator<UpdateAdminRoleDto> updateRoleValidator,
    IValidator<UpdatePlanDto> updatePlanValidator) : BaseController
{
    private Guid UserId => Guid.Parse(
        User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

    [HttpPost("admins/invite")]
    public async Task<IActionResult> InviteAdmin(
        [FromBody] InviteAdminDto dto)
    {
        await ValidateAsync(inviteValidator, dto);
        await managementService.InviteAdminAsync(dto, UserId);
        return NoContent();
    }

    [HttpGet("admins")]
    public async Task<IActionResult> GetAllAdmins()
    {
        var result = await managementService.GetAllAdminsAsync();
        return Ok(result);
    }

    [HttpDelete("admins/{id}")]
    public async Task<IActionResult> DeleteAdmin(Guid id)
    {
        await managementService.DeleteAdminAsync(id);
        return NoContent();
    }

    [HttpPatch("admins/{id}/role")]
    public async Task<IActionResult> UpdateAdminRole(
        Guid id, [FromBody] UpdateAdminRoleDto dto)
    {
        await ValidateAsync(updateRoleValidator, dto);
        await managementService.UpdateAdminRoleAsync(id, dto);
        return NoContent();
    }

    [HttpPatch("plans/{id}")]
    public async Task<IActionResult> UpdatePlan(
        Guid id, [FromBody] UpdatePlanDto dto)
    {
        await ValidateAsync(updatePlanValidator, dto);
        await managementService.UpdatePlanAsync(id, dto);
        return NoContent();
    }

    [HttpGet("stats")]
    public async Task<IActionResult> GetStats()
    {
        var result = await managementService.GetStatsAsync();
        return Ok(result);
    }
}