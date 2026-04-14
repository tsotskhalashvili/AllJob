using AllJob.Domain.Enums.Auth;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AllJob.API.Controllers;

public abstract class AdminBaseController : BaseController
{
    protected Guid UserId => Guid.Parse(
        User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

    protected bool IsSuperAdmin()
        => User.IsInRole("SuperAdmin");

    protected AdminRole? GetAdminRole()
    {
        var value = User.FindFirst("AdminRole")?.Value;
        return Enum.TryParse<AdminRole>(value, out var role) ? role : null;
    }

    protected bool HasAccess(params AdminRole[] allowedRoles)
        => IsSuperAdmin() ||
           (GetAdminRole().HasValue &&
            allowedRoles.Contains(GetAdminRole()!.Value));
}