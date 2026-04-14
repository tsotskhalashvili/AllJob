using AllJob.Domain.Enums.Auth;

namespace AllJob.Application.DTOs.Management;

public record InviteAdminDto(
    string Email,
    AdminRole Role
);