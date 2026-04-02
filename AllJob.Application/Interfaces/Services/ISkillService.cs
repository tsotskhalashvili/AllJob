using AllJob.Application.DTOs.Skill;

namespace AllJob.Application.Interfaces.Services;

public interface ISkillService
{
    Task<IReadOnlyList<SkillResponseDto>> GetAllAsync();
    Task<SkillResponseDto> CreateAsync(CreateSkillDto dto);
}