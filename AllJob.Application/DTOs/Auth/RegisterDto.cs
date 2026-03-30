namespace AllJob.Application.DTOs.Auth;

public record RegisterDto(
    string Email,
    string Password,
    string Role
    );

