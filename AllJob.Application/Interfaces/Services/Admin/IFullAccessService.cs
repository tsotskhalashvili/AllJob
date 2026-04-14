using AllJob.Application.DTOs.Admin;

namespace AllJob.Application.Interfaces.Services.Admin;

public interface IFullAccessService
{
    Task<AdminStatsDto> GetAdminStatsAsync();
}