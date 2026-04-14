using AllJob.Application.DTOs.Management;
using AllJob.Domain.Entities.Auth;
using AllJob.Domain.Entities.Subscriptions;

namespace AllJob.Application.Mappings;

public static class ManagementMappings
{
    public static AdminResponseDto ToAdminDto(this User user)
        => new(
            Id: user.Id,
            Email: user.Email,
            FirstName: user.AdminProfile!.FirstName,
            LastName: user.AdminProfile!.LastName,
            Role: user.AdminRole!.Value,
            CreatedAt: user.CreatedAt
        );

    public static void UpdateEntity(
        this Plan plan, UpdatePlanDto dto)
    {
        if (dto.Price is not null)
            plan.Price = dto.Price.Value;

        if (dto.MaxJobListings is not null)
            plan.MaxJobListings = dto.MaxJobListings.Value;
    }
}