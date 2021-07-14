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
            source.Cover = CheckForUploading(source.Cover);
            await base.Create(source);
        }

        public async Task Update(int id, BookForm source)
        {
            source.Cover = CheckForUploading(source.Cover);

            await base.Update(id, source);
        }

        public static int GCD(int a, int b)
        {
            while (a != b)
            {
                if (a > b)
                {
                    a -= b;
                }
                else
                {
                    b -= a;
                }
            }
            return a;
        }

        public string CheckForUploading(string urlOrBase64)
        {
            if (urlOrBase64.EndsWith(".jpg"))
                return urlOrBase64;

            var base64Header = "base64,";
            var imageHeader = "image/";
            var fileExtension = urlOrBase64.Substring(urlOrBase64.IndexOf(imageHeader) + imageHeader.Length, urlOrBase64.IndexOf(base64Header) - base64Header.Length);

            var imageName =   Guid.NewGuid().ToString("N") + "." + fileExtension;
            var imageData = urlOrBase64.Substring(urlOrBase64.IndexOf(base64Header) + 1 + base64Header.Length - 1);
            byte[] fileData = Convert.FromBase64String(imageData);

            //write image
            using (var imageFile = new FileStream(imageName, FileMode.Create))
            {
                var ms = new MemoryStream(fileData);
                Image img = Image.FromStream(ms);
                var imageSize = new Tuple<int, int>(img.Width, img.Height);

                var limitSize = imageConfig.LimitSize;
                var gcd = GCD(imageSize.Item1, imageSize.Item2);
                var scale = new Tuple<int, int>(imageSize.Item1 / gcd, imageSize.Item2 / gcd);

                if (imageSize.Item1 > limitSize || imageSize.Item2 > limitSize)
                {
                    if (imageSize.Item1 >= imageSize.Item2)
                    {
                        img = new Bitmap(img, new System.Drawing.Size(limitSize, limitSize * scale.Item2 / scale.Item1));
                    }
                    else if (imageSize.Item2 > imageSize.Item1)
                    {
                        img = new Bitmap(img, new System.Drawing.Size(limitSize * scale.Item2 / scale.Item1, limitSize));
                    }
                }
                
                img.Save(imageFile, System.Drawing.Imaging.ImageFormat.Jpeg);
            }

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(imageName),
                UseFilename = true,
                UniqueFilename = false
            };

            var account = new Account(
                imageConfig.CloudinaryCloud,
                imageConfig.ApiKey,
                imageConfig.ApiSecret);

            Cloudinary cloudinary = new(account);
            cloudinary.Api.Secure = true;
            cloudinary.Upload(uploadParams);
            
            return imageName;
        }

    }
}
