namespace AllJob.Application.DTOs.Messaging;

public record CreateConversationDto(
    Guid CandidateId,
    Guid EmployerId
);