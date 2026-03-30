using AllJob.Domain.Enums;

namespace AllJob.Application.DTOs.Application;


public record UpdateApplicationStatusDto(
    ApplicationStatus Status
);
