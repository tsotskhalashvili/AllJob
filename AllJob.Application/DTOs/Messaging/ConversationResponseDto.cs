namespace AllJob.Application.DTOs.Messaging;

public record ConversationResponseDto(
    Guid Id,
    Guid CandidateId,
    Guid EmployerId,

     string CandidateName,
    string CandidatePhotoUrl,
    string EmployerName,

    DateTime LastMessageAt,
    MessageResponseDto? LastMessage
);