namespace AllJob.Application.DTOs.Management;

public record AcceptInviteDto(
    string Token,
    string FirstName,
    string LastName,
    string Password
);