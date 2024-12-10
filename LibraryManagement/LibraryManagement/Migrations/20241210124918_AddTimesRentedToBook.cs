using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddTimesRentedToBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2603ee69-6a91-4f6d-ae65-cee3f79fbe5e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "81cf09cf-c868-4c9c-b455-d2591c2d7d4b");

            migrationBuilder.AddColumn<int>(
                name: "TimesRented",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "55f14005-a573-4ce1-a68c-09885a2c4218", null, "User", "USER" },
                    { "791c9948-8877-4585-a5ba-4e4d23005304", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "55f14005-a573-4ce1-a68c-09885a2c4218");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "791c9948-8877-4585-a5ba-4e4d23005304");

            migrationBuilder.DropColumn(
                name: "TimesRented",
                table: "Books");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2603ee69-6a91-4f6d-ae65-cee3f79fbe5e", null, "Admin", "ADMIN" },
                    { "81cf09cf-c868-4c9c-b455-d2591c2d7d4b", null, "User", "USER" }
                });
        }
    }
}
