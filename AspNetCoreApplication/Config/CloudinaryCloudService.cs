using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApplication.Config
{
    public class CloudinaryCloudService
    {
        private readonly ImageConfig imageConfig;
        public CloudinaryCloudService(ImageConfig imageConfig)
        {
            this.imageConfig = imageConfig;
        }
        public async Task<string> UploadImage(string imageName, byte[] data)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(imageName, new MemoryStream(data.ToArray())),
                UseFilename = true,
                UniqueFilename = false
            };

            var account = new Account(
                imageConfig.CloudinaryCloud,
                imageConfig.ApiKey,
                imageConfig.ApiSecret);

            Cloudinary cloudinary = new(account);
            cloudinary.Api.Secure = true;
            var result = await cloudinary.UploadAsync(uploadParams);
            return result.SecureUrl.ToString();
        }
    }
}
