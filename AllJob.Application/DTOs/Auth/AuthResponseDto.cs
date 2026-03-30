namespace AllJob.Application.DTOs.Auth;

public record AuthResponseDto(
    string AccessToken,
    string RefreshToken,
    DateTime ExpiresAt
);