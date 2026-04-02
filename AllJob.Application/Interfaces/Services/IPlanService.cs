using AllJob.Application.DTOs.Subscription;

namespace AllJob.Application.Interfaces.Services;

public interface IPlanService
{
    Task<IReadOnlyList<PlanResponseDto>> GetAllAsync();
}