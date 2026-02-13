using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TestWebApp.Migrations
{
    /// <inheritdoc />
    public partial class SeedRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "17d17983-542d-4746-bbcb-2082afda0e83", "0cdf06d5-b48a-4f1c-a667-d1d667d76b26", "Admin", "ADMIN" },
                    { "7e2bbba5-49df-4790-b210-bd307d77d3fd", "81257563-923e-494b-921a-bd29865a3124", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "17d17983-542d-4746-bbcb-2082afda0e83");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7e2bbba5-49df-4790-b210-bd307d77d3fd");
        }
    }
}
