namespace AllJob.Application.Interfaces.Services.Candidate;

public interface ICvGenerationService
{
    Task<string> GenerateCvAsync(Guid userId, string lang); 
}