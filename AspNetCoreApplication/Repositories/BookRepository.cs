using AspNetCoreApplication.DTO.DTObook;
using AspNetCoreApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Threading.Tasks;
using AspNetCoreApplication.Exceptions;
using System.IO;
using System.Configuration;
using Microsoft.Web.Administration;
using Microsoft.Extensions.Options;
using AutoMapper.Configuration;
using AspNetCoreApplication.Config;
using System.Text.RegularExpressions;
using System.Drawing.Imaging;

namespace AspNetCoreApplication.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        private readonly ImageConfig imageConfig;
        public BookRepository(DataContext dataContext, ImageConfig imageConfig) : base(dataContext)
        {
            this.imageConfig = imageConfig;
        }

        public async Task Create(BookForm source)
        {
            source.Cover = await CheckForUploading(source.Cover);
            await base.Create(source);
        }

        public async Task Update(int id, BookForm source)
        {
            source.Cover = await CheckForUploading(source.Cover);

            await base.Update(id, source);
        }

        public async Task<string> CheckForUploading(string urlOrBase64)
        {
            if (string.IsNullOrEmpty(urlOrBase64))
            {
                return string.Empty;
            }

            if(new Regex("http(s)?://").IsMatch(urlOrBase64))
            {
                return urlOrBase64;
            }

            var base64Header = "base64,";
            var imageHeader = "image/";
            var fileExtension = urlOrBase64.Substring(urlOrBase64.IndexOf(imageHeader) + imageHeader.Length, urlOrBase64.IndexOf(base64Header) - base64Header.Length);

            var imageName =   Guid.NewGuid().ToString("N") + "." + fileExtension;
            var imageData = urlOrBase64.Substring(urlOrBase64.IndexOf(base64Header) + 1 + base64Header.Length - 1);
            var fileData = Convert.FromBase64String(imageData);

            var memoryStream = new MemoryStream(fileData);
            Image img = Image.FromStream(memoryStream);
            
            var widthLimit = imageConfig.LimitSize[0];
            var heightLimit = imageConfig.LimitSize[1];

            img = ReSize(img, widthLimit, heightLimit);

            var tempMemoryStream = new MemoryStream();
            img.Save(tempMemoryStream, ImageFormat.Jpeg);

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(imageName, new MemoryStream(tempMemoryStream.ToArray())),
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

        public Image ReSize(Image image, int widthLimit, int heightLimit)
        {
            if (image.Width > widthLimit) image = new Bitmap(image, new System.Drawing.Size(widthLimit, image.Height * widthLimit / image.Width));
            if (image.Height > heightLimit) image = new Bitmap(image, new System.Drawing.Size(image.Width * heightLimit / image.Height, heightLimit));

            return image;
        }
    }
}
