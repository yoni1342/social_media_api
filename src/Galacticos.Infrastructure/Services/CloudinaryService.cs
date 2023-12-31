using Galacticos.Application.Services.ImageUpload;
using Microsoft.AspNetCore.Http;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Galacticos.Application.Cloudinary;
using Microsoft.Extensions.Options;

namespace Galacticos.Infrastructure.Services;

public class CloudinaryService : ICloudinaryService
{
    private CloudinarySettings _cloudinarySettings { get; }

    public CloudinaryService(IOptions<CloudinarySettings> cloudinarySettings)
    {
        _cloudinarySettings = cloudinarySettings.Value;
    }

    public Task<string> UploadImageAsync(IFormFile imageFile)
    {
        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
    var extension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();
    
    if (!allowedExtensions.Contains(extension))
    {
        throw new ArgumentException("Invalid file type. Only JPG and PNG files are allowed.");
    }
        var client = new Cloudinary(new Account(
            _cloudinarySettings.CloudName = "dsyi5xipw",
            _cloudinarySettings.ApiKey = "954213167565616",
            _cloudinarySettings.ApiSecret = "0Mo5_8OwrtF667fAKJ938qCjmxI"
        ));

        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(imageFile.FileName, imageFile.OpenReadStream()),
            Transformation = new Transformation().Height(500).Width(500).Crop("fill")
        };

        var uploadResult = client.UploadAsync(uploadParams);

        return Task.FromResult(uploadResult.Result.SecureUrl.AbsoluteUri);
    }
}