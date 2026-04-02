namespace AllJob.Application.DTOs.Admin;

public record UserResponseDto(
    Guid Id,
    string Email,
    bool IsActive,
    DateTime CreatedAt
);