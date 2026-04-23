namespace AllJob.Application.DTOs.Messaging;

public record ConversationResponseDto(
    Guid Id,
    Guid CandidateId,
    Guid EmployerId,
    DateTime LastMessageAt,
    MessageResponseDto? LastMessage
);