using AllJob.Domain.Enums.Auth;

namespace AllJob.Application.DTOs.Management;

public record AdminResponseDto(
    Guid Id,
    string Email,
    AdminRole Role,
    DateTime CreatedAt
);