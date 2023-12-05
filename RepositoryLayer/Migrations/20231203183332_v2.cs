using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EFLayer.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "76f86073-b51c-47c4-b7fa-731628055ebb");

            migrationBuilder.InsertData(
                table: "ApplicationRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Date", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3f3ee49a-4441-4303-b550-84d57255a24c", null, "12/3/2023 8:33:32 PM", "Patient", "Patient" },
                    { "596a7b4b-2907-4729-adbb-866cad99185c", null, "12/3/2023 8:33:32 PM", "Doctor", "Doctor" },
                    { "5ab58670-8727-4b67-85d5-4199912a70bf", null, "12/3/2023 8:33:32 PM", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId", "Discriminator" },
                values: new object[] { "5ab58670-8727-4b67-85d5-4199912a70bf", "76f86073-b51c-47c4-b7fa-731628055ebb", "IdentityUserRole<string>" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApplicationRole",
                keyColumn: "Id",
                keyValue: "3f3ee49a-4441-4303-b550-84d57255a24c");

            migrationBuilder.DeleteData(
                table: "ApplicationRole",
                keyColumn: "Id",
                keyValue: "596a7b4b-2907-4729-adbb-866cad99185c");

            migrationBuilder.DeleteData(
                table: "ApplicationRole",
                keyColumn: "Id",
                keyValue: "5ab58670-8727-4b67-85d5-4199912a70bf");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "5ab58670-8727-4b67-85d5-4199912a70bf", "76f86073-b51c-47c4-b7fa-731628055ebb" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "FullName", "Gender", "Image", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "Phone", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "SecurityStamp", "TwoFactorEnabled", "Type", "UserName" },
                values: new object[] { "76f86073-b51c-47c4-b7fa-731628055ebb", 0, "d37a443e-b4a7-456a-8cf2-f91e7431d41c", null, "admin@gmail.com", true, null, null, null, true, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAEP67oJbqNSLOUeeUOjBJX1oz9wxhrOyuo1ilgByurG0N8eXBqPdENDj2HD4rVCgzgQ==", null, null, false, null, "3fe49b40-216f-4c9f-91c7-a660862f51a1", false, 0, "admin" });
        }
    }
}
