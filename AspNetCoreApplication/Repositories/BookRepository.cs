using AspNetCoreApplication.DTO.DTOBook;
using AspNetCoreApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Threading.Tasks;
using AspNetCoreApplication.Exceptions;
using System.IO;
using System.Configuration;
using Microsoft.Web.Administration;
using Microsoft.Extensions.Options;
using AutoMapper.Configuration;
using AspNetCoreApplication.Services;
using System.Text.RegularExpressions;
using System.Drawing.Imaging;
using AspNetCoreApplication.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using AspNetCoreApplication.DTO.DTOuser;

namespace AspNetCoreApplication.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        private readonly ICloudinaryService cloudService;

        private readonly IAuthenticationService authenticationService;

        private readonly DataContext dataContext;

        public BookRepository(DataContext dataContext,
            ICloudinaryService cloudService,
            IAuthenticationService authenticationService) : base(dataContext)
        {
            this.cloudService = cloudService;
            this.dataContext = dataContext;
            this.authenticationService = authenticationService;
        }

        public async Task Create(BookForm source)
        {
            source.Cover = await CheckForUploading(source.Cover);
            await base.Create(source);
        }

        public async Task Update(int id, BookForm source)
        {
            await CheckRole(id);

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

            var resizedImage = img.EnsureImageSizeLimit();

            var dataUpload = resizedImage.ImageToByteArray();

            var uploadResult = await cloudService.UploadImage(imageName, dataUpload);

            return uploadResult;
        }

        public async Task<List<Category>> GetCategories(int bookId)
        {
            return await dataContext.Books
                .Where(x => x.Id == bookId)
                .SelectMany(x => x.Categories.Select(y => y.Category))
                .ToListAsync();
        }

        public override async Task Delete(int id)
        {
            await CheckRole(id);
            await base.Delete(id);
        }

        public async Task CheckRole(int id)
        {
            var currentUser = authenticationService.CurrentUser;
            var bookDetail = await base.GetByIdOrThrow(id);

            if (currentUser.Role == Role.Admin) return;
            if (currentUser.Role == Role.Manager) return;

            if (bookDetail.UserId != currentUser.Id)
            {
                throw new UnauthorizedException("Bạn không có quyền hạn thay đổi sách này.");
            }
        }

        public async Task<List<BookDedicated>> Get()
        {
            var listBooks = await base.Get<BookDedicated>();
            foreach(var item in listBooks)
            {
                item.Categories = await GetCategories(item.Id);
            }
            return listBooks;
        }
        public async Task<BookDedicated> Get(int id)
        {
            var book = await base.Get<BookDedicated>(id);
            book.Categories = await GetCategories(book.Id);
            return book.ConvertTo<BookDedicated>();
        }
    }
}
