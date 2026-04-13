using AllJob.Application.DTOs.Candidate;
using AllJob.Application.Exceptions;
using AllJob.Application.Interfaces;
using AllJob.Application.Interfaces.Repositories;
using AllJob.Application.Interfaces.Repositories.Candidate;
using AllJob.Application.Interfaces.Services.Candidate;
using AllJob.Application.Mappings;
using AllJob.Domain.Entities.Candidate;

namespace AllJob.Application.Services.Candidate;

public class CandidateExperienceService(
    ICandidateRepository candidateRepository,
    IGenericRepository<CandidateExperience> experienceRepository,
    IUnitOfWork unitOfWork) : ICandidateExperienceService
{
    public async Task AddExperienceAsync(ExperienceDto dto, Guid userId)
    {
        var candidate = await candidateRepository
            .GetCandidateWithDetailsAsync(userId)
            ?? throw new NotFoundException("CandidateProfile", userId);

        var experience = dto.ToEntity(candidate.Id);
        await experienceRepository.AddAsync(experience);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateExperienceAsync(
    Guid experienceId, ExperienceDto dto, Guid userId)
    {
        var candidate = await candidateRepository
            .GetCandidateWithDetailsAsync(userId)
            ?? throw new NotFoundException("CandidateProfile", userId);

        var experience = await experienceRepository
            .GetByIdAsync(experienceId)
            ?? throw new NotFoundException("Experience", experienceId);

        if (experience.CandidateProfileId != candidate.Id)
            throw new ForbiddenException();

        experience.CompanyName = dto.CompanyName;
        experience.Position = dto.Position;
        experience.StartDate = dto.StartDate;
        experience.EndDate = dto.EndDate;

        experienceRepository.Update(experience);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteExperienceAsync(Guid experienceId, Guid userId)
    {
        var candidate = await candidateRepository
            .GetCandidateWithDetailsAsync(userId)
            ?? throw new NotFoundException("CandidateProfile", userId);

        var experience = await experienceRepository
            .GetByIdAsync(experienceId)
            ?? throw new NotFoundException("Experience", experienceId);

        if (experience.CandidateProfileId != candidate.Id)
            throw new ForbiddenException();

        experienceRepository.Delete(experience);
        await unitOfWork.SaveChangesAsync();
    }
}