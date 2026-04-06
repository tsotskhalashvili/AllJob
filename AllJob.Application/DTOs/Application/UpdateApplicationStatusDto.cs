using AllJob.Domain.Enums.Applications;

namespace AllJob.Application.DTOs.Application;


public record UpdateApplicationStatusDto(
    ApplicationStatus Status
);
