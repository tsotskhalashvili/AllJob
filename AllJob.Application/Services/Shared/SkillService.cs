using AllJob.Application.DTOs.Skill;
using AllJob.Application.Interfaces;
using AllJob.Application.Interfaces.Repositories;
using AllJob.Application.Interfaces.Services.Shared;
using AllJob.Application.Mappings;
using AllJob.Domain.Entities.Jobs;

namespace AllJob.Application.Services.Shared;

public class SkillService(
    IGenericRepository<Skill> skillRepository,
    IUnitOfWork unitOfWork) : ISkillService
{
    public async Task<IReadOnlyList<SkillResponseDto>> GetAllAsync()
    {
        var skills = await skillRepository.GetAllAsync();
        return skills.Select(s => s.ToDto()).ToList();
    }

    public async Task<SkillResponseDto> CreateAsync(CreateSkillDto dto)
    {
        var skill = dto.ToEntity();
        await skillRepository.AddAsync(skill);
        await unitOfWork.SaveChangesAsync();
        return skill.ToDto();
    }
}