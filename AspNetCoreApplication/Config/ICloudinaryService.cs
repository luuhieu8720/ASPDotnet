using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApplication.Config
{
    public interface ICloudinaryService
    {
        public Task<string> UploadImage(string imageName, byte[] data);
    }
}
