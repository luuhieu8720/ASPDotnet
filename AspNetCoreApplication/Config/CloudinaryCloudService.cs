using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApplication.Config
{
    public class CloudinaryCloudService : ICloudinaryService
    {
        private readonly Cloudinary cloudinary;
        public CloudinaryCloudService(ImageConfig imageConfig)
        {
            var account = new Account(
                imageConfig.CloudinaryCloud,
                imageConfig.ApiKey,
                imageConfig.ApiSecret);
            this.cloudinary = new Cloudinary(account);
        }
        public async Task<string> UploadImage(string imageName, byte[] data)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(imageName, new MemoryStream(data)),
                UseFilename = true,
                UniqueFilename = false
            };

            cloudinary.Api.Secure = true;
            var result = await cloudinary.UploadAsync(uploadParams);
            return result.SecureUrl.ToString();
        }
    }
}
