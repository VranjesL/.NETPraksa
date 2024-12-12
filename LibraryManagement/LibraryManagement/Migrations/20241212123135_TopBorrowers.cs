using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagement.Migrations
{
    /// <inheritdoc />
    public partial class TopBorrowers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "55f14005-a573-4ce1-a68c-09885a2c4218");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "791c9948-8877-4585-a5ba-4e4d23005304");

            migrationBuilder.AddColumn<int>(
                name: "TotalRentals",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "98b56e1f-c48f-45bb-965c-ab578643e8c4", null, "Admin", "ADMIN" },
                    { "a70a9700-828d-4c1e-861a-5f9d8398e84d", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "98b56e1f-c48f-45bb-965c-ab578643e8c4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a70a9700-828d-4c1e-861a-5f9d8398e84d");

            migrationBuilder.DropColumn(
                name: "TotalRentals",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "55f14005-a573-4ce1-a68c-09885a2c4218", null, "User", "USER" },
                    { "791c9948-8877-4585-a5ba-4e4d23005304", null, "Admin", "ADMIN" }
                });
        }
    }
}
