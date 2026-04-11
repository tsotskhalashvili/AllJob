using Microsoft.AspNetCore.Http;

namespace AllJob.Application.Interfaces.Services.Shared;

public interface IFileUploadService
{
    Task<string> UploadImageAsync(IFormFile file);
    Task DeleteImageAsync(string publicId);
}
