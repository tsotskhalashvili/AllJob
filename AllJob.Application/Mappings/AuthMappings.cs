using AllJob.Application.DTOs.Auth;
using AllJob.Domain.Entities.Auth;

namespace AllJob.Application.Mappings;

public static class AuthMappings
{
    public static User ToEntity(this RegisterDto dto)
        => new()
        {
            Email = dto.Email,
            IsActive = true,
            IsPasswordChangeRequired = false

        };
}
