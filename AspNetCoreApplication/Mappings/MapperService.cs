using AspNetCoreApplication.DTO.DTOauthor;
using AspNetCoreApplication.DTO.DTOcategory;
using AspNetCoreApplication.DTO.DTOuser;
using AspNetCoreApplication.DTO.DTObook;
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

namespace AspNetCoreApplication.Mappings
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
            cfg.CreateMap<UserForm, User> ();
            cfg.CreateMap<User, UserDetail>();
            cfg.CreateMap<User, UserItem>();
            cfg.CreateMap<Book, BookItem>();
            cfg.CreateMap<BookForm, Book>();
            cfg.CreateMap<Book, BookDetail>();
        }

        public static T MapTo<T>(this object source)
        {
            if(source == null)
            {
                throw new NullReferenceException();
            }

            return mapper.Map<T>(source);
        }
        public static void Copy(this object source, object dest)
        {
            var sourceProperties = source.GetType().GetProperties();
            var destProperties = dest.GetType().GetProperties();

            foreach (var sourceProperty in sourceProperties)
            {
                foreach (var destProperty in destProperties)
                {
                    if (sourceProperty.Name == destProperty.Name && sourceProperty.PropertyType == destProperty.PropertyType)
                    {
                        destProperty.SetValue(dest, sourceProperty.GetValue(source));
                        break;
                    }
                }
            }
        }
    }
}
