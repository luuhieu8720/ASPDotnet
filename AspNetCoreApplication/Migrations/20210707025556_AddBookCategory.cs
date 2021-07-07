using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetCoreApplication.Migrations
{
    public partial class AddBookCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookCategory_Books_BooksId",
                table: "BookCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_BookCategory_Categories_CategoriesId",
                table: "BookCategory");

            migrationBuilder.RenameColumn(
                name: "CategoriesId",
                table: "BookCategory",
                newName: "BookId");

            migrationBuilder.RenameColumn(
                name: "BooksId",
                table: "BookCategory",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_BookCategory_CategoriesId",
                table: "BookCategory",
                newName: "IX_BookCategory_BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookCategory_Books_BookId",
                table: "BookCategory",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookCategory_Categories_CategoryId",
                table: "BookCategory",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookCategory_Books_BookId",
                table: "BookCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_BookCategory_Categories_CategoryId",
                table: "BookCategory");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "BookCategory",
                newName: "CategoriesId");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "BookCategory",
                newName: "BooksId");

            migrationBuilder.RenameIndex(
                name: "IX_BookCategory_BookId",
                table: "BookCategory",
                newName: "IX_BookCategory_CategoriesId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookCategory_Books_BooksId",
                table: "BookCategory",
                column: "BooksId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookCategory_Categories_CategoriesId",
                table: "BookCategory",
                column: "CategoriesId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
