using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreApplication.Models;

namespace AspNetCoreApplication
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<BookCategory>().HasKey(x => new
            {
                x.CategoryId,
                x.BookId
            });
            modelBuilder.Entity<BookCategory>()
            .HasOne<Category>(i => i.Category)
            .WithMany(c => c.Books)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BookCategory>()
            .HasOne<Book>(i => i.Book)
            .WithMany(c => c.Categories)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
    }
}
