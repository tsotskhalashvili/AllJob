using AllJob.Domain.Enums.Auth;

namespace AllJob.Application.DTOs.Management;

public record AdminResponseDto(
    Guid Id,
    string Email,
    string FirstName,
    string LastName,
    AdminRole Role,
    DateTime CreatedAt
);