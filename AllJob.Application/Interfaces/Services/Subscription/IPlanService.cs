using AllJob.Application.DTOs.Subscription;

namespace AllJob.Application.Interfaces.Services.Subscription;

public interface IPlanService
{
    Task<IReadOnlyList<PlanResponseDto>> GetAllAsync();
}