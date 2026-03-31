using AllJob.Application.DTOs.Candidate;
using AllJob.Domain.Entities.Candidate;

namespace AllJob.Application.Mappings;

public static class CandidateMappings
{
    public static CandidateResponseDto ToDto(this CandidateProfile candidate)
        => new(
            Id: candidate.Id,
            FirstName: candidate.FirstName,
            LastName: candidate.LastName,
            Bio: candidate.Bio,
            LinkedInUrl: candidate.LinkedInUrl,
            PhotoUrl: candidate.PhotoUrl,
            Country: candidate.Address.Country,
            City: candidate.Address.City,
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
            CompanyName: experience.CompanyName,
            Position: experience.Position,
            StartDate: experience.StartDate,
            EndDate: experience.EndDate
        );

    public static EducationDto ToDto(this CandidateEducation education)
        => new(
            University: education.University,
            Degree: education.Degree,
            FieldOfStudy: education.FieldOfStudy,
            StartDate: education.StartDate,
            EndDate: education.EndDate
        );

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
        candidate.FirstName = dto.FirstName;
        candidate.LastName = dto.LastName;
        candidate.Bio = dto.Bio;
        candidate.LinkedInUrl = dto.LinkedInUrl;
        candidate.PhotoUrl = dto.PhotoUrl;
        candidate.AddressId = dto.AddressId;
    }
}