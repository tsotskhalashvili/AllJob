namespace AllJob.Application.DTOs.Auth;

public record GoogleAuthDto(
    string IdToken,
    string Role

);