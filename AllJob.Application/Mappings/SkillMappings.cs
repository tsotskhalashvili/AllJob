using AllJob.Application.DTOs.Skill;
using AllJob.Domain.Entities.Jobs;

namespace AllJob.Application.Mappings;

public static class SkillMappings
{
    public static SkillResponseDto ToDto(this Skill skill)
        => new(
            Id: skill.Id,
            Name: skill.Name
        );

    public static Skill ToEntity(this CreateSkillDto dto)
        => new()
        {
            Name = dto.Name
        };
}