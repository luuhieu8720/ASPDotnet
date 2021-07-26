using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetCoreApplication.Migrations
{
    public partial class AddBookCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookCategories_Books_BooksId",
                table: "BookCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_BookCategories_Categories_CategoriesId",
                table: "BookCategories");

            migrationBuilder.RenameColumn(
                name: "CategoriesId",
                table: "BookCategories",
                newName: "BookId");

            migrationBuilder.RenameColumn(
                name: "BooksId",
                table: "BookCategories",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_BookCategories_CategoriesId",
                table: "BookCategories",
                newName: "IX_BookCategories_BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookCategories_Books_BookId",
                table: "BookCategories",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookCategories_Categories_CategoryId",
                table: "BookCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookCategories_Books_BookId",
                table: "BookCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_BookCategories_Categories_CategoryId",
                table: "BookCategories");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "BookCategories",
                newName: "CategoriesId");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "BookCategories",
                newName: "BooksId");

            migrationBuilder.RenameIndex(
                name: "IX_BookCategories_BookId",
                table: "BookCategories",
                newName: "IX_BookCategories_CategoriesId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookCategories_Books_BooksId",
                table: "BookCategories",
                column: "BooksId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookCategories_Categories_CategoriesId",
                table: "BookCategories",
                column: "CategoriesId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
