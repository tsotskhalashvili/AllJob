using AllJob.Application.DTOs.Admin;
using AllJob.Domain.Entities.Auth;

namespace AllJob.Application.Mappings;

public static class AdminMappings
{
    public static UserResponseDto ToDto(this User user)
        => new(
            Id: user.Id,
            Email: user.Email,
            IsActive: user.IsActive,
            CreatedAt: user.CreatedAt
        );
}