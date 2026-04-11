using AllJob.Application.DTOs.Common;

namespace AllJob.Application.Interfaces.Services.Shared;

public interface IStatsService
{
    Task<PublicStatsDto> GetPublicStatsAsync();

}
