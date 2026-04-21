namespace AllJob.Application.DTOs.Auth;

public record VerifyTwoFactorDto(
    Guid UserId,
    string Otp
);