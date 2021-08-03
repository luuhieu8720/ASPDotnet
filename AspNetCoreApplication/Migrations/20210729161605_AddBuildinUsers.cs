using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetCoreApplication.Migrations
{
    public partial class AddBuildinUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Birthday", "LastOnline", "Name", "Password", "Role", "Username" },
                values: new object[,]
                {
                    { 1001, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "2e7a57d80b0fa48cee0e241c6866b9f9", 3, "admin" },
                    { 1002, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Contributor", "ee8eb04540a3f53cc360b6fe4db5a9f7", 1, "contributor" },
                    { 1003, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Manager", "55378fc28039a0d7b003a54fae7788e7", 2, "manager" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1001);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1002);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1003);
        }
    }
}
