using AllJob.Domain.Enums.Auth;

namespace AllJob.Application.DTOs.Management;

public record UpdateAdminRoleDto(
    AdminRole Role
);