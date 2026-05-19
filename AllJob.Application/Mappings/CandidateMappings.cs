using AllJob.Application.DTOs.Candidate;
using AllJob.Domain.Entities.Candidate;
using AllJob.Domain.Enums.Candidate;
using System.Diagnostics.Metrics;

namespace AllJob.Application.Mappings;

public static class CandidateMappings
{
    public static CandidateResponseDto ToDto(this CandidateProfile candidate)
        => new(
            Id: candidate.Id,
            FirstName: candidate.FirstName,
            LastName: candidate.LastName,
            Bio: candidate.Bio,
            LinkedInUrl: candidate.LinkedInUrl ?? string.Empty,
            PhotoUrl: candidate.PhotoUrl ?? string.Empty,
            Country:  candidate.Address?.Country ?? string.Empty,
            City: candidate.Address?.City ?? string.Empty,
            Skills: candidate.Skills
                .Select(cs => cs.Skill.Name)
                .ToList(),
            Experiences: candidate.Experiences
                .Select(e => e.ToDto())
                .ToList(),
            Educations: candidate.Educations
                .Select(e => e.ToDto())
                .ToList(),
            CreatedAt: candidate.CreatedAt
        );

    public static ExperienceDto ToDto(this CandidateExperience experience)
        => new(
            Id: experience.Id,
            CompanyName: experience.CompanyName,
            Position: experience.Position,
            StartDate: experience.StartDate,
            EndDate: experience.EndDate
        );

    public static CandidateExperience ToEntity(
    this ExperienceDto dto, Guid candidateProfileId)
    => new()
    {
        CandidateProfileId = candidateProfileId,
        CompanyName = dto.CompanyName,
        Position = dto.Position,
        StartDate = dto.StartDate,
        EndDate = dto.EndDate
    };

    public static EducationDto ToDto(this CandidateEducation education)
        => new(
            Id: education.Id,
            InstitutionName: education.InstitutionName,
            Degree: education.Degree.ToString(),
            FieldOfStudy: education.FieldOfStudy,
            StartDate: education.StartDate,
            EndDate: education.EndDate
        );

    public static CandidateEducation ToEntity(
    this EducationDto dto, Guid candidateProfileId)
    => new()
    {
        CandidateProfileId = candidateProfileId,
        InstitutionName = dto.InstitutionName,
        Degree = Enum.Parse<DegreeType>(dto.Degree),
        FieldOfStudy = dto.FieldOfStudy,
        StartDate = dto.StartDate,
        EndDate = dto.EndDate
    };

    

    public static CandidateProfile ToEntity(
        this CreateCandidateProfileDto dto, Guid userId)
        => new()
        {
            UserId = userId,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Bio = dto.Bio,
            LinkedInUrl = dto.LinkedInUrl,
            PhotoUrl = dto.PhotoUrl,
            AddressId = dto.AddressId
        };

    public static void UpdateEntity(
    this CandidateProfile candidate, UpdateCandidateProfileDto dto)
    {
        if (dto.FirstName is not null)
            candidate.FirstName = dto.FirstName;

        if (dto.LastName is not null)
            candidate.LastName = dto.LastName;

        if (dto.Bio is not null)
            candidate.Bio = dto.Bio;

        if (dto.LinkedInUrl is not null)
            candidate.LinkedInUrl = dto.LinkedInUrl;

        if (dto.PhotoUrl is not null)
            candidate.PhotoUrl = dto.PhotoUrl;

        if (dto.AddressId is not null)
            candidate.AddressId = dto.AddressId.Value;
    }
}