
using BusApi.Configs;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;

namespace BusApi.Settings.Implements
{
    public class CloudinarySetting : ICloudinarySetting
    {
        private readonly Cloudinary _cloudinary;

        public CloudinarySetting(IOptions<CloudinaryConfig> options)
        {
            Account account = new Account
            {
                Cloud = options.Value.CloudName,
                ApiKey = options.Value.ApiKey,
                ApiSecret = options.Value.ApiSecret
            };
            _cloudinary = new Cloudinary(account);
        }
        public async Task<ImageUploadResult> UploadImageAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParam = new ImageUploadParams { File = new FileDescription(file.FileName, stream), Folder = CloudinaryConfig.path };
                uploadResult = await _cloudinary.UploadAsync(uploadParam);
            }
            return uploadResult;
        }
    }
}
