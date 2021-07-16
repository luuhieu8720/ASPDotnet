using AspNetCoreApplication.DTO.DTOBook;
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
using AspNetCoreApplication.Extensions;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreApplication.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        private readonly ImageConfig imageConfig;

        private readonly ICloudinaryService cloudService;
        private readonly DataContext dataContext;
        public BookRepository(DataContext dataContext, ImageConfig imageConfig, ICloudinaryService cloudService) : base(dataContext)
        {
            this.imageConfig = imageConfig;
            this.cloudService = cloudService;
            this.dataContext = dataContext;
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

        private async Task<string> CheckForUploading(string urlOrBase64)
        {
            if (string.IsNullOrEmpty(urlOrBase64))
            {
                return string.Empty;
            }

            if (new Regex("http(s)?://").IsMatch(urlOrBase64))
            {
                return urlOrBase64;
            }

            var base64Header = "base64,";
            var imageHeader = "image/";
            var fileExtension = urlOrBase64.Substring(urlOrBase64.IndexOf(imageHeader) + imageHeader.Length, urlOrBase64.IndexOf(base64Header) - base64Header.Length);

            var imageName = Guid.NewGuid().ToString("N") + "." + fileExtension;
            var imageData = urlOrBase64.Substring(urlOrBase64.IndexOf(base64Header) + base64Header.Length);
            var fileData = Convert.FromBase64String(imageData);

            using var memoryStream = new MemoryStream(fileData);
            var img = Image.FromStream(memoryStream);

            var resizedImage = EnsureImageSizeLimit(img);

            var dataUpload = resizedImage.ImageToByteArray();

            var uploadResult = await cloudService.UploadImage(imageName,dataUpload);

            return uploadResult;
        }

        private Image EnsureImageSizeLimit(Image image)
        {
            var heightLimit = imageConfig.CoverLimitWidth;
            var widthLimit = imageConfig.CoverLimitHeight;

            if (image.Width < widthLimit && image.Height < heightLimit)
            {
                return image;
            }

            var scaleWidth = image.Width * 1.0 / widthLimit;
            var scaleHeight = image.Height * 1.0 / heightLimit;

            var scale = Math.Max(scaleWidth, scaleHeight);

            return new Bitmap(image, (int)(image.Width / scale), (int) (image.Height / scale));
        }
        public async Task<List<Category>> GetCategories(int bookId)
        {
            return await dataContext.BookCategories.Where(x => x.BookId == bookId)
                                                   .Select(x => x.Category)
                                                   .ToListAsync();
        }

    }
}
