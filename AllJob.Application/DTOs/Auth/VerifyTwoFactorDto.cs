namespace AllJob.Application.DTOs.Auth;

public record VerifyTwoFactorDto(
    string ChallengeToken,
    string Otp
);