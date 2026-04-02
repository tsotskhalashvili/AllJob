using AllJob.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllJob.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin,SuperAdmin")]
public class AdminController(IAdminService adminService) : BaseController
{
    [HttpGet("users")]
    public async Task<IActionResult> GetAllUsers()
    {
        var result = await adminService.GetAllUsersAsync();
        return Ok(result);
    }

    [HttpPatch("users/{id}/deactivate")]
    public async Task<IActionResult> DeactivateUser(Guid id)
    {
        await adminService.DeactivateUserAsync(id);
        return NoContent();
    }

    [HttpPatch("companies/{id}/verify")]
    public async Task<IActionResult> VerifyCompany(Guid id)
    {
        await adminService.VerifyCompanyAsync(id);
        return NoContent();
    }
}