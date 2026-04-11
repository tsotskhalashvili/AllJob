using AllJob.Application.DTOs.Common;

namespace AllJob.Application.Interfaces.Services;

public interface IStatsService
{
    Task<PublicStatsDto> GetPublicStatsAsync();

}
