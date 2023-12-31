using Microsoft.AspNetCore.Http;
namespace Galacticos.Application.Services.ImageUpload;

public interface ICloudinaryService
{
    Task<string> UploadImageAsync(IFormFile imageFile);
}
