namespace AllJob.Application.DTOs.Candidate;

public record UpdateCandidateSkillsDto(
    List<Guid> SkillIds
);