using AllJob.Application.DTOs.Candidate;
using AllJob.Application.Exceptions;
using AllJob.Application.Interfaces;
using AllJob.Application.Interfaces.Repositories.Candidate;
using AllJob.Application.Interfaces.Services.Candidate;
using AllJob.Application.Mappings;
using AllJob.Domain.Entities.Candidate;

namespace AllJob.Application.Services.Candidate;

public class CandidateService(
    ICandidateRepository candidateRepository,
    IUnitOfWork unitOfWork) : ICandidateService
{
    public async Task<CandidateResponseDto> GetProfileAsync(Guid userId)
    {
        var candidate = await candidateRepository
            .GetCandidateWithDetailsAsync(userId)
            ?? throw new NotFoundException("CandidateProfile", userId);

        return candidate.ToDto();
    }

    public async Task<CandidateResponseDto> CreateProfileAsync(
        CreateCandidateProfileDto dto, Guid userId)
    {
        var existing = await candidateRepository
            .GetCandidateWithDetailsAsync(userId);

        if (existing is not null)
            throw new ConflictException("Candidate profile already exists.");

        var candidate = dto.ToEntity(userId);

        candidate.Skills = dto.SkillIds
            .Select(skillId => new CandidateSkill
            {
                CandidateProfileId = candidate.Id,
                SkillId = skillId
            }).ToList();

        await candidateRepository.AddAsync(candidate);
        await unitOfWork.SaveChangesAsync();

        var created = await candidateRepository
            .GetCandidateWithDetailsAsync(userId)
            ?? throw new NotFoundException("CandidateProfile", userId);

        return created.ToDto();
    }

    public async Task<CandidateResponseDto> GetPublicProfileAsync(Guid candidateId)
    {
        var candidate = await candidateRepository
            .GetByIdWithDetailsAsync(candidateId)
            ?? throw new NotFoundException("CandidateProfile", candidateId);

        return candidate.ToDto();
    }
    public async Task<CandidateResponseDto> UpdateProfileAsync(
        UpdateCandidateProfileDto dto, Guid userId)
    {
        var candidate = await candidateRepository
            .GetCandidateWithDetailsAsync(userId)
            ?? throw new NotFoundException("CandidateProfile", userId);

        candidate.UpdateEntity(dto);
        candidateRepository.Update(candidate);
        await unitOfWork.SaveChangesAsync();

        var updated = await candidateRepository
            .GetCandidateWithDetailsAsync(userId)
            ?? throw new NotFoundException("CandidateProfile", userId);

        return updated.ToDto();
    }

    public async Task UpdateSkillsAsync(UpdateCandidateSkillsDto dto, Guid userId)
{
    var candidate = await candidateRepository
        .GetCandidateWithSkillsTrackedAsync(userId)
        ?? throw new NotFoundException("CandidateProfile", userId);

    candidate.Skills.Clear();

    candidate.Skills = dto.SkillIds
        .Select(skillId => new CandidateSkill
        {
            CandidateProfileId = candidate.Id,
            SkillId = skillId
        }).ToList();

    candidateRepository.Update(candidate);
    await unitOfWork.SaveChangesAsync();
}

    public async Task DeleteProfileAsync(Guid userId)
    {
        var candidate = await candidateRepository
            .GetCandidateWithDetailsAsync(userId)
            ?? throw new NotFoundException("CandidateProfile", userId);

        candidateRepository.Delete(candidate);
        await unitOfWork.SaveChangesAsync();
    }
}