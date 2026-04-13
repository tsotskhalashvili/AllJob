using AllJob.Application.DTOs.Candidate;
using AllJob.Application.Exceptions;
using AllJob.Application.Interfaces;
using AllJob.Application.Interfaces.Repositories;
using AllJob.Application.Interfaces.Repositories.Candidate;
using AllJob.Application.Interfaces.Services.Candidate;
using AllJob.Application.Mappings;
using AllJob.Domain.Entities.Candidate;
using AllJob.Domain.Enums.Candidate;

namespace AllJob.Application.Services.Candidate;

public class CandidateEducationService(
    ICandidateRepository candidateRepository,
    IGenericRepository<CandidateEducation> educationRepository,
    IUnitOfWork unitOfWork) : ICandidateEducationService
{
    public async Task AddEducationAsync(EducationDto dto, Guid userId)
    {
        var candidate = await candidateRepository
            .GetCandidateWithDetailsAsync(userId)
            ?? throw new NotFoundException("CandidateProfile", userId);

        var education = dto.ToEntity(candidate.Id);
        await educationRepository.AddAsync(education);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task UpdateEducationAsync(
    Guid educationId, EducationDto dto, Guid userId)
    {
        var candidate = await candidateRepository
            .GetCandidateWithDetailsAsync(userId)
            ?? throw new NotFoundException("CandidateProfile", userId);

        var education = await educationRepository
            .GetByIdAsync(educationId)
            ?? throw new NotFoundException("Education", educationId);

        if (education.CandidateProfileId != candidate.Id)
            throw new ForbiddenException();

        education.InstitutionName = dto.InstitutionName;
        education.Degree = Enum.Parse<DegreeType>(dto.Degree);
        education.FieldOfStudy = dto.FieldOfStudy;
        education.StartDate = dto.StartDate;
        education.EndDate = dto.EndDate;

        educationRepository.Update(education);
        await unitOfWork.SaveChangesAsync();
    }
    public async Task DeleteEducationAsync(Guid educationId, Guid userId)
    {
        var candidate = await candidateRepository
            .GetCandidateWithDetailsAsync(userId)
            ?? throw new NotFoundException("CandidateProfile", userId);

        var education = await educationRepository
            .GetByIdAsync(educationId)
            ?? throw new NotFoundException("Education", educationId);

        if (education.CandidateProfileId != candidate.Id)
            throw new ForbiddenException();

        educationRepository.Delete(education);
        await unitOfWork.SaveChangesAsync();
    }
}