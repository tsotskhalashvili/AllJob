using AllJob.Application.Interfaces.Services.Shared;
using AllJob.Application.Settings;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;

namespace AllJob.API.Services;

public class CloudinaryService (IOptions<CloudinarySettings> settings)
    : IFileUploadService
{
    private readonly Cloudinary _cloudinary = new Cloudinary(new Account(
        settings.Value.CloudName,
        settings.Value.ApiKey,
        settings.Value.ApiSecret
        ));
    public async Task<string> UploadImageAsync(IFormFile file)
    {
        using var stream = file.OpenReadStream();

        var uploadParms = new ImageUploadParams
        {
            File = new FileDescription(file.FileName, stream),

            Folder = "alljob",

            Transformation = new Transformation()
             .Width(500)
             .Height(500)
             .Crop("fill")
        };

        var result = await _cloudinary.UploadAsync(uploadParms);
        return result.SecureUrl.ToString();

    }
    public async Task DeleteImageAsync(string publicId)
    {
        var deleteParms = new DeletionParams(publicId);

        await _cloudinary.DestroyAsync(deleteParms);
    }

}
