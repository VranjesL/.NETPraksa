using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagement.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "65b0859a-d053-47fe-a1ba-0e8ca4aa21bf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c91338a4-42f2-453d-adb5-8b9f32c8d838");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "aa2457e0-83e9-4d01-bdf6-39b26d6dcf9a", null, "User", "USER" },
                    { "ea59eef5-a799-4e13-9f14-38d587ab372e", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa2457e0-83e9-4d01-bdf6-39b26d6dcf9a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ea59eef5-a799-4e13-9f14-38d587ab372e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "65b0859a-d053-47fe-a1ba-0e8ca4aa21bf", null, "User", "USER" },
                    { "c91338a4-42f2-453d-adb5-8b9f32c8d838", null, "Admin", "ADMIN" }
                });
        }
    }
}
