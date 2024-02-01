using CloudinaryDotNet.Actions;

namespace BusApi.Settings
{
    public interface ICloudinarySetting
    {
        Task<ImageUploadResult> UploadImageAsync(IFormFile file);
    }
}
