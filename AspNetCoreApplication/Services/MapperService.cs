using AspNetCoreApplication.DTO.DTOAuthor;
using AspNetCoreApplication.DTO.DTOCategory;
using AspNetCoreApplication.DTO.DTOUser;
using AspNetCoreApplication.DTO.DTOBook;
using AspNetCoreApplication.Models;
using AutoMapper;
using AutoMapper.Configuration;
using AutoMapper.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace AspNetCoreApplication.Services
{
    public static class MapperService
    {
        private static readonly MapperConfiguration config = new(CreateMap);
        private static readonly IMapper mapper = config.CreateMapper();

        private static void CreateMap(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<AuthorForm, Author>();
            cfg.CreateMap<Author, AuthorDetail>();
            cfg.CreateMap<Author, AuthorItem>();
            cfg.CreateMap<CategoryForm, Category>();
            cfg.CreateMap<Category, CategoryDetail>();
            cfg.CreateMap<Category, CategoryItem>();
            cfg.CreateMap<UserForm, User>();
            cfg.CreateMap<User, UserDetail>();
            cfg.CreateMap<User, UserItem>();
            cfg.CreateMap<BookForm, Book>();
            cfg.CreateMap<Book, BookDetail>()
                .ForMember(bookDetail => bookDetail.Categories,
                option => option.MapFrom(book => book.Categories
                                                .Select(x => x.Category
                                                .ConvertTo<CategoryDetail>()))); ;
            cfg.CreateMap<Book, BookItem>()
                .ForMember(bookItem => bookItem.Categories,
                option => option.MapFrom(book => book.Categories
                                                .Select(x => x.Category
                                                .ConvertTo<CategoryItem>())));
        }

        public static T ConvertTo<T>(this object source)
        {
            if (source == null)
            {
                throw new NullReferenceException();
            }

            return mapper.Map<T>(source);
        }
        public static void CopyTo(this object source, object destination)
        {
            if (source == null)
            {
                throw new NullReferenceException("Source can't be null");
            }
            if (destination == null)
            {
                throw new NullReferenceException("Destination can't be null");
            }
            mapper.Map(source, destination);
        }
    }
}