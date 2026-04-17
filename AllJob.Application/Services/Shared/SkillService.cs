using AllJob.Application.DTOs.Skill;
using AllJob.Application.Interfaces;
using AllJob.Application.Interfaces.Repositories;
using AllJob.Application.Interfaces.Services.Shared;
using AllJob.Application.Mappings;
using AllJob.Domain.Entities.Jobs;

namespace AllJob.Application.Services.Shared;

public class SkillService(
    IGenericRepository<Skill> skillRepository,
    ICacheService cacheService,
    IUnitOfWork unitOfWork) : ISkillService
{
    private const string CacheKey = "skills:all";
    public async Task<IReadOnlyList<SkillResponseDto>> GetAllAsync()
    {
        var cached = cacheService.Get<IReadOnlyList<SkillResponseDto>>(CacheKey);
        if (cached is not null) return cached;

        var skills = await skillRepository.GetAllAsync();
        var result = skills.Select(s => s.ToDto()).ToList();

        cacheService.Set(CacheKey, result, TimeSpan.FromHours(1));

        return result;
    }

    public async Task<SkillResponseDto> CreateAsync(CreateSkillDto dto)
    {
        var skill = dto.ToEntity();
        await skillRepository.AddAsync(skill);
        await unitOfWork.SaveChangesAsync();

        cacheService.Remove(CacheKey);
        return skill.ToDto();
    }
}